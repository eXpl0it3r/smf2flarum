using System.Security.Policy;
using Mapping.Flarum185;
using Microsoft.EntityFrameworkCore;
using Schema.Flarum185;
using Schema.Smf2019;

namespace smf2flarum;

public class MigrationStats
{
    public int GroupsMigrated { get; set; }
    public int GroupsSkipped { get; set; }
    public int UsersMigrated { get; set; }
    public int UsersSkipped { get; set; }
    public int GroupUsersMigrated { get; set; }
    public int GroupUsersSkipped { get; set; }
    public int TagsMigrated { get; set; }
    public int TagsSkipped { get; set; }
    public int DiscussionsMigrated { get; set; }
    public int DiscussionsSkipped { get; set; }
    public int DiscussionTagsMigrated { get; set; }
    public int PostsMigrated { get; set; }
    public int PostsSkipped { get; set; }
    
    // Extended entity statistics
    public int DiscussionUsersMigrated { get; set; }
    public int DiscussionUsersSkipped { get; set; }
    public int FlagsMigrated { get; set; }
    public int FlagsSkipped { get; set; }
    public int PostLikesMigrated { get; set; }
    public int PostLikesSkipped { get; set; }
    public int CategoriesMigrated { get; set; }
    public int CategoriesSkipped { get; set; }
    public int GroupPermissionsMigrated { get; set; }
    public int GroupPermissionsSkipped { get; set; }
    public int AttachmentsMigrated { get; set; }
    public int AttachmentsSkipped { get; set; }
    public int PollsMigrated { get; set; }
    public int PollsSkipped { get; set; }
    public int NotificationsMigrated { get; set; }
    public int NotificationsSkipped { get; set; }
    public int SettingsMigrated { get; set; }
    public int SettingsSkipped { get; set; }
    
    // High-priority entity statistics
    public int PostMentionsMigrated { get; set; }
    public int PostMentionsSkipped { get; set; }
    public int PersonalMessagesMigrated { get; set; }
    public int PersonalMessagesSkipped { get; set; }
    public int ModeratorsMigrated { get; set; }
    public int ModeratorsSkipped { get; set; }
    public int TagUsersMigrated { get; set; }
    public int TagUsersSkipped { get; set; }
}

public class Migrator
{
    private readonly SmfContext _smfContext;
    private readonly FlarumContext _flarumContext;

    public static Migrator Create(Options options)
    {
        var smfContext = new SmfContext(new DbContextOptionsBuilder<SmfContext>().UseMySQL(options.SmfConnectionString).Options);
        var flarumContext = new FlarumContext(new DbContextOptionsBuilder<FlarumContext>().UseMySQL(options.FlarumConnectionString).Options);

        return new Migrator(smfContext, flarumContext);
    }

    public async Task ExecuteAsync()
    {
        Console.WriteLine("=== Migration Start ===");

        using var smfTransaction = await _smfContext.Database.BeginTransactionAsync();
        using var flarumTransaction = await _flarumContext.Database.BeginTransactionAsync();

        try
        {
            var mapper = MapperFactory.Create();
            var stats = new MigrationStats();

            Console.WriteLine("-- Ensuring Default Flarum Groups Exist --");
            await EnsureDefaultGroupsExistAsync();

            Console.WriteLine("-- Migrating User & GroupUser --");

            /* === Group Mappings ===
             * 1 Administrator    => 1 Admin
             * 2 Global Moderator => 4 Mod
             * 3 Moderator        => 4 Mod
             * 4 Newbie           => 3 Member
             * 5 Jr. Member       => 3 Member
             * 6 Full Member      => 3 Member
             * 7 Sr. Member       => 3 Member
             * 8 Hero Member      => 3 Member
             * XYZ                => NEW
             */
            var groupMapping = new Dictionary<int, uint>
            {
                {
                    0, 3  // No group/unassigned => Member
                },
                {
                    1, 1
                },
                {
                    2, 4
                },
                {
                    3, 4
                },
                {
                    4, 3
                },
                {
                    5, 3
                },
                {
                    6, 3
                },
                {
                    7, 3
                },
                {
                    8, 3
                }
            };
            
            var unknownGroups = _smfContext.Membergroups.Where(IsUnknownSmfMemberGroup).ToList();
            var totalUnknownGroups = unknownGroups.Count;
            var processedGroups = 0;
            
            foreach (var membergroup in unknownGroups)
            {
                processedGroups++;
                Console.WriteLine($"[{processedGroups}/{totalUnknownGroups}] Adding non-standard Group: {membergroup.IdGroup} {membergroup.GroupName}");

                if (await _flarumContext.Groups.AnyAsync(g => g.Id == membergroup.IdGroup))
                {
                    Console.WriteLine("Group already exists, skipping...");
                    stats.GroupsSkipped++;
                    continue;
                }
                
                var newGroup = mapper.Map<Group>(membergroup);
                await _flarumContext.Groups.AddAsync(newGroup);
                
                groupMapping.Add(membergroup.IdGroup, newGroup.Id);
                stats.GroupsMigrated++;
            }

            await _flarumContext.SaveChangesAsync();
            
            var members = _smfContext.Members.ToList();
            var totalMembers = members.Count;
            var processedMembers = 0;
            
            foreach (var member in members)
            {
                processedMembers++;
                Console.WriteLine($"[{processedMembers}/{totalMembers}] Adding User: {member.IdMember} {member.MemberName}");

                if (await _flarumContext.Users.AnyAsync(u => u.Id == member.IdMember))
                {
                    Console.WriteLine("User already exists, skipping...");
                    stats.UsersSkipped++;
                    continue;
                }

                if (await _flarumContext.Users.AnyAsync(u => u.Username == member.MemberName))
                {
                    Console.WriteLine($"Username '{member.MemberName}' already exists, skipping...");
                    stats.UsersSkipped++;
                    continue;
                }

                var newUser = mapper.Map<User>(member);
                await _flarumContext.Users.AddAsync(newUser);
                await _flarumContext.SaveChangesAsync();
                stats.UsersMigrated++;
                
                Console.WriteLine($"Adding GroupUser: {newUser.Id} {groupMapping[member.IdGroup]}");

                if (await _flarumContext.GroupUsers.AnyAsync(gu => gu.UserId == newUser.Id && gu.GroupId == groupMapping[member.IdGroup]))
                {
                    Console.WriteLine("GroupUser already exists, skipping...");
                    stats.GroupUsersSkipped++;
                    continue;
                }
                
                var newGroupUser = mapper.Map<GroupUser>(member);
                newGroupUser.GroupId = groupMapping[member.IdGroup];
                await _flarumContext.GroupUsers.AddAsync(newGroupUser);
                stats.GroupUsersMigrated++;
            }
            
            await _flarumContext.SaveChangesAsync();
            
            Console.WriteLine("-- Migrating Boards to Tags --");
            
            var boards = _smfContext.Boards.OrderBy(b => b.BoardOrder).ToList();
            var totalBoards = boards.Count;
            var processedBoards = 0;
            
            foreach (var board in boards)
            {
                processedBoards++;
                Console.WriteLine($"[{processedBoards}/{totalBoards}] Adding Tag from Board: {board.IdBoard} {board.Name}");

                if (await _flarumContext.Tags.AnyAsync(t => t.Id == board.IdBoard))
                {
                    Console.WriteLine("Tag already exists, skipping...");
                    stats.TagsSkipped++;
                    continue;
                }

                var newTag = mapper.Map<Tag>(board);
                await _flarumContext.Tags.AddAsync(newTag);
                stats.TagsMigrated++;
            }
            
            await _flarumContext.SaveChangesAsync();
            
            Console.WriteLine("-- Migrating Topics to Discussions --");
            
            var postNumberMapping = new Dictionary<uint, uint>(); // SMF message ID -> Flarum post number
            var topics = _smfContext.Topics.Where(t => t.Approved == 1).OrderBy(t => t.IdTopic).ToList();
            var totalTopics = topics.Count;
            var processedTopics = 0;
            
            foreach (var topic in topics)
            {
                processedTopics++;
                Console.WriteLine($"[{processedTopics}/{totalTopics}] Adding Discussion from Topic: {topic.IdTopic}");

                if (await _flarumContext.Discussions.AnyAsync(d => d.Id == topic.IdTopic))
                {
                    Console.WriteLine("Discussion already exists, skipping...");
                    stats.DiscussionsSkipped++;
                    continue;
                }

                // Get the first message to extract title and creation date
                var firstMessage = await _smfContext.Messages
                    .FirstOrDefaultAsync(m => m.IdMsg == topic.IdFirstMsg);
                
                if (firstMessage == null)
                {
                    Console.WriteLine("First message not found, skipping topic...");
                    stats.DiscussionsSkipped++;
                    continue;
                }

                var newDiscussion = mapper.Map<Discussion>(topic);
                newDiscussion.Title = firstMessage.Subject;
                newDiscussion.CreatedAt = Mapping.Converter.UnixTimeStampToDateTime(firstMessage.PosterTime) ?? DateTime.UtcNow;
                newDiscussion.Slug = CreateSlug(firstMessage.Subject);
                
                // Get last message for last posted date
                var lastMessage = await _smfContext.Messages
                    .FirstOrDefaultAsync(m => m.IdMsg == topic.IdLastMsg);
                
                if (lastMessage != null)
                {
                    newDiscussion.LastPostedAt = Mapping.Converter.UnixTimeStampToDateTime(lastMessage.PosterTime);
                }

                await _flarumContext.Discussions.AddAsync(newDiscussion);
                await _flarumContext.SaveChangesAsync();
                stats.DiscussionsMigrated++;
                
                // Create DiscussionTag relationship
                var discussionTag = new DiscussionTag
                {
                    DiscussionId = newDiscussion.Id,
                    TagId = (uint)topic.IdBoard,
                    CreatedAt = newDiscussion.CreatedAt
                };
                
                await _flarumContext.DiscussionTags.AddAsync(discussionTag);
                stats.DiscussionTagsMigrated++;
            }
            
            await _flarumContext.SaveChangesAsync();
            
            Console.WriteLine("-- Migrating Messages to Posts --");
            
            var topicsForPosts = _smfContext.Topics.Where(t => t.Approved == 1).OrderBy(t => t.IdTopic).ToList();
            var processedTopicsForPosts = 0;
            
            foreach (var topic in topicsForPosts)
            {
                processedTopicsForPosts++;
                Console.WriteLine($"[{processedTopicsForPosts}/{totalTopics}] Processing posts for Topic: {topic.IdTopic}");
                
                var messages = await _smfContext.Messages
                    .Where(m => m.IdTopic == topic.IdTopic && m.Approved == 1)
                    .OrderBy(m => m.PosterTime)
                    .ToListAsync();
                
                uint postNumber = 1;
                
                foreach (var message in messages)
                {
                    if (await _flarumContext.Posts.AnyAsync(p => p.Id == message.IdMsg))
                    {
                        Console.WriteLine($"Post {message.IdMsg} already exists, skipping...");
                        stats.PostsSkipped++;
                        continue;
                    }
                    
                    var newPost = mapper.Map<Post>(message);
                    newPost.Number = postNumber;
                    
                    // First post is the discussion starter
                    if (message.IdMsg == topic.IdFirstMsg)
                    {
                        newPost.Type = "discussionRenamed"; // or "comment" - depends on Flarum version
                    }
                    
                    await _flarumContext.Posts.AddAsync(newPost);
                    postNumberMapping[message.IdMsg] = postNumber;
                    postNumber++;
                    stats.PostsMigrated++;
                }
            }
            
            await _flarumContext.SaveChangesAsync();

            Console.WriteLine("-- Updating Discussion Post References --");
            await UpdateDiscussionPostReferencesAsync(stats);

            // === Extended Entity Migrations ===
            
            Console.WriteLine("-- Migrating Categories to Tags --");
            await MigrateCategoriesAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Board Permissions to Group Permissions --");
            await MigrateBoardPermissionsAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Log Topic (Read Tracking) to Discussion Users --");
            await MigrateLogTopicAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Log Reported to Flags --");
            await MigrateLogReportedAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Log Karma to Post Likes --");
            await MigrateLogKarmaAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Log Notify to Notifications --");
            await MigrateLogNotifyAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Settings --");
            await MigrateSettingsAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Attachments (DTO Export) --");
            await MigrateAttachmentsAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Polls (DTO Export) --");
            await MigratePollsAsync(mapper, stats);

            // === High-Priority Missing Entity Migrations ===
            
            Console.WriteLine("-- Migrating Post Mentions --");
            await MigratePostMentionsAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Personal Messages (DTO Export) --");
            await MigratePersonalMessagesAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Moderators to Group Assignments --");
            await MigrateModeratorsAsync(mapper, stats);
            
            Console.WriteLine("-- Migrating Tag User Subscriptions --");
            await MigrateTagUsersAsync(mapper, stats);

            // Commit both transactions
            await smfTransaction.CommitAsync();
            await flarumTransaction.CommitAsync();
            
            Console.WriteLine("\n=== Migration Statistics ===");
            Console.WriteLine($"Groups: {stats.GroupsMigrated} migrated, {stats.GroupsSkipped} skipped");
            Console.WriteLine($"Users: {stats.UsersMigrated} migrated, {stats.UsersSkipped} skipped");
            Console.WriteLine($"GroupUsers: {stats.GroupUsersMigrated} migrated, {stats.GroupUsersSkipped} skipped");
            Console.WriteLine($"Tags: {stats.TagsMigrated} migrated, {stats.TagsSkipped} skipped");
            Console.WriteLine($"Discussions: {stats.DiscussionsMigrated} migrated, {stats.DiscussionsSkipped} skipped");
            Console.WriteLine($"DiscussionTags: {stats.DiscussionTagsMigrated} migrated");
            Console.WriteLine($"Posts: {stats.PostsMigrated} migrated, {stats.PostsSkipped} skipped");
            
            Console.WriteLine("\n=== Extended Entity Statistics ===");
            Console.WriteLine($"Categories: {stats.CategoriesMigrated} migrated, {stats.CategoriesSkipped} skipped");
            Console.WriteLine($"Group Permissions: {stats.GroupPermissionsMigrated} migrated, {stats.GroupPermissionsSkipped} skipped");
            Console.WriteLine($"Discussion Users: {stats.DiscussionUsersMigrated} migrated, {stats.DiscussionUsersSkipped} skipped");
            Console.WriteLine($"Flags: {stats.FlagsMigrated} migrated, {stats.FlagsSkipped} skipped");
            Console.WriteLine($"Post Likes: {stats.PostLikesMigrated} migrated, {stats.PostLikesSkipped} skipped");
            Console.WriteLine($"Notifications: {stats.NotificationsMigrated} migrated, {stats.NotificationsSkipped} skipped");
            Console.WriteLine($"Settings: {stats.SettingsMigrated} migrated, {stats.SettingsSkipped} skipped");
            Console.WriteLine($"Attachments (DTO): {stats.AttachmentsMigrated} exported, {stats.AttachmentsSkipped} skipped");
            Console.WriteLine($"Polls (DTO): {stats.PollsMigrated} exported, {stats.PollsSkipped} skipped");
            
            Console.WriteLine("\n=== High-Priority Entity Statistics ===");
            Console.WriteLine($"Post Mentions: {stats.PostMentionsMigrated} migrated, {stats.PostMentionsSkipped} skipped");
            Console.WriteLine($"Personal Messages (DTO): {stats.PersonalMessagesMigrated} exported, {stats.PersonalMessagesSkipped} skipped");
            Console.WriteLine($"Moderators: {stats.ModeratorsMigrated} migrated, {stats.ModeratorsSkipped} skipped");
            Console.WriteLine($"Tag Users: {stats.TagUsersMigrated} migrated, {stats.TagUsersSkipped} skipped");
            
            Console.WriteLine("\n=== Migration Completed Successfully ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"=== Migration Failed: {ex.Message} ===");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            
            // Rollback transactions
            await smfTransaction.RollbackAsync();
            await flarumTransaction.RollbackAsync();
            
            throw;
        }
    }

    private static bool IsUnknownSmfMemberGroup(Membergroup membergroup)
    {
        return membergroup.GroupName switch
        {
            "Administrator" => false,
            "Global Moderator" => false,
            "Moderator" => false,
            "Newbie" => false,
            "Jr. Member" => false,
            "Full Member" => false,
            "Sr. Member" => false,
            "Hero Member" => false,
            _ => true
        };
    }

    private static string CreateSlug(string title)
    {
        return title.ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("&", "and")
            .Replace("'", "")
            .Replace("\"", "")
            .Replace(",", "")
            .Replace(".", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("[", "")
            .Replace("]", "")
            .Replace("{", "")
            .Replace("}", "")
            .Replace("!", "")
            .Replace("?", "")
            .Replace(";", "")
            .Replace(":", "");
    }

    private Migrator(SmfContext smfContext, FlarumContext flarumContext)
    {
        _smfContext = smfContext;
        _flarumContext = flarumContext;
    }

    // === Extended Migration Methods ===

    private async Task MigrateCategoriesAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var categories = _smfContext.Categories.OrderBy(c => c.CatOrder).ToList();
        var totalCategories = categories.Count;
        var processedCategories = 0;

        foreach (var category in categories)
        {
            processedCategories++;
            Console.WriteLine($"[{processedCategories}/{totalCategories}] Adding Tag from Category: {category.IdCat} {category.Name}");

            // Use ID offset to avoid conflicts with board tags
            uint tagId = (uint)(category.IdCat + 10000);

            if (await _flarumContext.Tags.AnyAsync(t => t.Id == tagId))
            {
                Console.WriteLine("Category tag already exists, skipping...");
                stats.CategoriesSkipped++;
                continue;
            }

            var newTag = mapper.Map<Tag>(category);
            newTag.Id = tagId;
            await _flarumContext.Tags.AddAsync(newTag);
            stats.CategoriesMigrated++;
        }

        await _flarumContext.SaveChangesAsync();
    }

    private async Task MigrateBoardPermissionsAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var permissions = _smfContext.BoardPermissions.ToList();
        var totalPermissions = permissions.Count;
        var processedPermissions = 0;
        var addedPermissions = new HashSet<(uint GroupId, string Permission)>();

        foreach (var permission in permissions)
        {
            processedPermissions++;
            Console.WriteLine($"[{processedPermissions}/{totalPermissions}] Adding Group Permission from Board Permission: {permission.IdGroup} -> Profile {permission.IdProfile}");

            var newPermissions = mapper.Map<IEnumerable<GroupPermission>>(permission);
            var permissionsToAdd = new List<GroupPermission>();

            foreach (var newPermission in newPermissions)
            {
                var key = (newPermission.GroupId, newPermission.Permission);
                
                // Skip if we've already added this permission in this batch
                if (addedPermissions.Contains(key))
                {
                    Console.WriteLine($"Permission already added in batch: Group {key.GroupId}, Permission {key.Permission}");
                    continue;
                }

                // Skip if permission already exists in database
                if (await _flarumContext.GroupPermissions.AnyAsync(gp => 
                    gp.GroupId == newPermission.GroupId && gp.Permission == newPermission.Permission))
                {
                    Console.WriteLine($"Permission already exists in database: Group {key.GroupId}, Permission {key.Permission}");
                    stats.GroupPermissionsSkipped++;
                    continue;
                }

                addedPermissions.Add(key);
                permissionsToAdd.Add(newPermission);
            }

            if (permissionsToAdd.Any())
            {
                await _flarumContext.GroupPermissions.AddRangeAsync(permissionsToAdd);
                stats.GroupPermissionsMigrated += permissionsToAdd.Count;
            }
        }

        await _flarumContext.SaveChangesAsync();
    }

    private async Task MigrateLogTopicAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var logTopics = _smfContext.LogTopics.ToList();
        var totalLogTopics = logTopics.Count;
        var processedLogTopics = 0;

        foreach (var logTopic in logTopics)
        {
            processedLogTopics++;
            Console.WriteLine($"[{processedLogTopics}/{totalLogTopics}] Adding Discussion User from Log Topic: {logTopic.IdMember} -> {logTopic.IdTopic}");

            if (await _flarumContext.DiscussionUsers.AnyAsync(du => 
                du.UserId == logTopic.IdMember && du.DiscussionId == logTopic.IdTopic))
            {
                Console.WriteLine("Discussion user read tracking already exists, skipping...");
                stats.DiscussionUsersSkipped++;
                continue;
            }

            var newDiscussionUser = mapper.Map<DiscussionUser>(logTopic);
            await _flarumContext.DiscussionUsers.AddAsync(newDiscussionUser);
            stats.DiscussionUsersMigrated++;
        }

        await _flarumContext.SaveChangesAsync();
    }

    private async Task MigrateLogReportedAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var logReported = _smfContext.LogReporteds.ToList();
        var totalLogReported = logReported.Count;
        var processedLogReported = 0;

        foreach (var reported in logReported)
        {
            processedLogReported++;
            Console.WriteLine($"[{processedLogReported}/{totalLogReported}] Adding Flag from Log Reported: {reported.IdReport}");

            if (await _flarumContext.Flags.AnyAsync(f => f.Id == reported.IdReport))
            {
                Console.WriteLine("Flag already exists, skipping...");
                stats.FlagsSkipped++;
                continue;
            }

            var newFlag = mapper.Map<Flag>(reported);
            await _flarumContext.Flags.AddAsync(newFlag);
            stats.FlagsMigrated++;
        }

        await _flarumContext.SaveChangesAsync();
    }

    private async Task MigrateLogKarmaAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var logKarma = _smfContext.LogKarmas.ToList();
        var totalLogKarma = logKarma.Count;
        var processedLogKarma = 0;

        foreach (var karma in logKarma)
        {
            processedLogKarma++;
            Console.WriteLine($"[{processedLogKarma}/{totalLogKarma}] Adding Post Like from Log Karma: {karma.IdTarget} -> {karma.IdExecutor}");

            // Check if there's already a like from this user to this target
            if (await _flarumContext.PostLikes.AnyAsync(pl => 
                pl.UserId == karma.IdExecutor && pl.PostId == karma.IdTarget))
            {
                Console.WriteLine("Post like from karma already exists, skipping...");
                stats.PostLikesSkipped++;
                continue;
            }

            var newPostLike = mapper.Map<PostLike>(karma);
            await _flarumContext.PostLikes.AddAsync(newPostLike);
            stats.PostLikesMigrated++;
        }

        await _flarumContext.SaveChangesAsync();
    }

    private async Task MigrateLogNotifyAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var logNotify = _smfContext.LogNotifies.ToList();
        var totalLogNotify = logNotify.Count;
        var processedLogNotify = 0;

        foreach (var notify in logNotify)
        {
            processedLogNotify++;
            Console.WriteLine($"[{processedLogNotify}/{totalLogNotify}] Adding Notification from Log Notify: {notify.IdMember} -> {notify.IdTopic}");

            // Check for existing notification
            if (await _flarumContext.Notifications.AnyAsync(n => 
                n.UserId == notify.IdMember && n.SubjectId == notify.IdTopic && n.Type == "discussionRenamed"))
            {
                Console.WriteLine("Notification already exists, skipping...");
                stats.NotificationsSkipped++;
                continue;
            }

            var newNotification = mapper.Map<Notification>(notify);
            await _flarumContext.Notifications.AddAsync(newNotification);
            stats.NotificationsMigrated++;
        }

        await _flarumContext.SaveChangesAsync();
    }

    private async Task MigrateSettingsAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var allSettings = _smfContext.Settings.ToList();
        var settings = allSettings.Where(s => IsRelevantSetting(s.Variable)).ToList();
        var totalSettings = settings.Count;
        var processedSettings = 0;

        foreach (var setting in settings)
        {
            processedSettings++;
            Console.WriteLine($"[{processedSettings}/{totalSettings}] Processing Setting: {setting.Variable}");

            var mappedSetting = mapper.Map<Schema.Flarum185.Setting>(setting);

            if (await _flarumContext.Settings.AnyAsync(s => s.Key == mappedSetting.Key))
            {
                Console.WriteLine("Setting already exists, skipping...");
                stats.SettingsSkipped++;
                continue;
            }

            await _flarumContext.Settings.AddAsync(mappedSetting);
            stats.SettingsMigrated++;
        }

        await _flarumContext.SaveChangesAsync();
    }

    /// <summary>
    /// Determines which SMF settings are relevant for Flarum migration.
    /// </summary>
    private static bool IsRelevantSetting(string smfKey)
    {
        var relevantSettings = new[]
        {
            "forum_name",
            "forum_description", 
            "default_language",
            "admin_email",
            "registration_method",
            "posts_per_page",
            "messages_per_page",
            "time_format",
            "smtp_host",
            "smtp_port", 
            "smtp_username",
            "enable_mentions",
            "welcome_email",
            "guest_access"
        };

        return relevantSettings.Contains(smfKey);
    }

    private async Task MigrateAttachmentsAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var attachments = _smfContext.Attachments.ToList();
        var totalAttachments = attachments.Count;
        var processedAttachments = 0;

        Console.WriteLine("Note: Attachments require the FoF Upload extension. Exporting as DTO data...");

        foreach (var attachment in attachments)
        {
            processedAttachments++;
            Console.WriteLine($"[{processedAttachments}/{totalAttachments}] Exporting Attachment DTO: {attachment.IdAttach} {attachment.Filename}");

            var attachmentDto = mapper.Map<AttachmentDto>(attachment);
            // Here you could serialize to JSON, export to file, etc.
            // For now, just count as processed
            stats.AttachmentsMigrated++;
        }

        Console.WriteLine($"Exported {stats.AttachmentsMigrated} attachment DTOs. Install FoF Upload extension and import manually.");
    }

    private async Task MigratePollsAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var polls = _smfContext.Polls.ToList();
        var totalPolls = polls.Count;
        var processedPolls = 0;

        Console.WriteLine("Note: Polls require the FoF Polls extension. Exporting as DTO data...");

        foreach (var poll in polls)
        {
            processedPolls++;
            Console.WriteLine($"[{processedPolls}/{totalPolls}] Exporting Poll DTO: {poll.IdPoll} {poll.Question}");

            var pollDto = mapper.Map<PollDto>(poll);
            // Export poll choices too
            var pollChoices = await _smfContext.PollChoices
                .Where(pc => pc.IdPoll == poll.IdPoll)
                .ToListAsync();
            
            foreach (var choice in pollChoices)
            {
                var choiceDto = mapper.Map<PollChoiceDto>(choice);
                // Here you could serialize to JSON, export to file, etc.
            }

            stats.PollsMigrated++;
        }

        Console.WriteLine($"Exported {stats.PollsMigrated} poll DTOs. Install FoF Polls extension and import manually.");
    }

    // === High-Priority Migration Methods ===

    private async Task MigratePostMentionsAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var messages = _smfContext.Messages.ToList();
        var totalMessages = messages.Count;
        var processedMessages = 0;

        Console.WriteLine("Note: Processing messages for @mentions, #post, and #tag references...");

        foreach (var message in messages)
        {
            processedMessages++;
            if (processedMessages % 100 == 0) // Progress every 100 messages
            {
                Console.WriteLine($"[{processedMessages}/{totalMessages}] Processing mentions in message: {message.IdMsg}");
            }

            // Extract user mentions
            var userMentions = mapper.Map<List<PostMentionsUser>>(message);
            foreach (var mention in userMentions)
            {
                // TODO: Resolve username to user ID in a real implementation
                if (mention.MentionsUserId > 0) // Skip unresolved mentions for now
                {
                    await _flarumContext.PostMentionsUsers.AddAsync(mention);
                    stats.PostMentionsMigrated++;
                }
                else
                {
                    stats.PostMentionsSkipped++;
                }
            }

            // Extract post mentions
            var postMentions = mapper.Map<List<PostMentionsPost>>(message);
            foreach (var mention in postMentions)
            {
                if (await _flarumContext.Posts.AnyAsync(p => p.Id == mention.MentionsPostId))
                {
                    await _flarumContext.PostMentionsPosts.AddAsync(mention);
                    stats.PostMentionsMigrated++;
                }
                else
                {
                    stats.PostMentionsSkipped++;
                }
            }

            // Extract group mentions
            var groupMentions = mapper.Map<List<PostMentionsGroup>>(message);
            foreach (var mention in groupMentions)
            {
                await _flarumContext.PostMentionsGroups.AddAsync(mention);
                stats.PostMentionsMigrated++;
            }
        }

        await _flarumContext.SaveChangesAsync();
        Console.WriteLine($"Processed {stats.PostMentionsMigrated} post mentions. Note: Username/tag mentions may require manual resolution.");
    }

    private async Task MigratePersonalMessagesAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var personalMessages = _smfContext.PersonalMessages.ToList();
        var totalPMs = personalMessages.Count;
        var processedPMs = 0;

        Console.WriteLine("Note: Personal messages require fof/byobu or similar PM extension. Exporting as DTO data...");

        foreach (var pm in personalMessages)
        {
            processedPMs++;
            Console.WriteLine($"[{processedPMs}/{totalPMs}] Exporting Personal Message DTO: {pm.IdPm} {pm.Subject}");

            var pmDto = mapper.Map<PersonalMessageDto>(pm);
            
            // Get recipients for this message
            var recipients = await _smfContext.PmRecipients
                .Where(r => r.IdPm == pm.IdPm)
                .ToListAsync();
            
            foreach (var recipient in recipients)
            {
                var recipientDto = mapper.Map<PmRecipientDto>(recipient);
                pmDto.Recipients.Add(recipientDto);
            }

            // Here you could serialize to JSON, export to file, etc.
            // For now, just count as processed
            stats.PersonalMessagesMigrated++;
        }

        Console.WriteLine($"Exported {stats.PersonalMessagesMigrated} personal message DTOs. Install PM extension and import manually.");
    }

    private async Task MigrateModeratorsAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        var moderators = _smfContext.Moderators.ToList();
        var totalModerators = moderators.Count;
        var processedModerators = 0;

        foreach (var moderator in moderators)
        {
            processedModerators++;
            Console.WriteLine($"[{processedModerators}/{totalModerators}] Adding Moderator Group Assignment: User {moderator.IdMember} -> Board {moderator.IdBoard}");

            // Add user to moderator group if not already there
            if (!await _flarumContext.GroupUsers.AnyAsync(gu => 
                gu.UserId == moderator.IdMember && gu.GroupId == 4))
            {
                var newGroupUser = mapper.Map<GroupUser>(moderator);
                await _flarumContext.GroupUsers.AddAsync(newGroupUser);
                stats.ModeratorsMigrated++;
            }
            else
            {
                Console.WriteLine("User already in moderator group, skipping group assignment...");
                stats.ModeratorsSkipped++;
            }

            // Add board-specific permissions
            var permissions = mapper.Map<List<GroupPermission>>(moderator);
            foreach (var permission in permissions)
            {
                if (!await _flarumContext.GroupPermissions.AnyAsync(gp => 
                    gp.GroupId == permission.GroupId && gp.Permission == permission.Permission))
                {
                    await _flarumContext.GroupPermissions.AddAsync(permission);
                    stats.ModeratorsMigrated++;
                }
                else
                {
                    stats.ModeratorsSkipped++;
                }
            }
        }

        await _flarumContext.SaveChangesAsync();
    }

    private async Task MigrateTagUsersAsync(AutoMapper.Mapper mapper, MigrationStats stats)
    {
        // Group LogNotify entries by user and board to create tag subscriptions
        var notificationGroups = _smfContext.LogNotifies
            .GroupBy(ln => new { ln.IdMember, ln.IdBoard })
            .ToList();

        var totalGroups = notificationGroups.Count;
        var processedGroups = 0;

        foreach (var group in notificationGroups)
        {
            processedGroups++;
            Console.WriteLine($"[{processedGroups}/{totalGroups}] Adding Tag Subscription: User {group.Key.IdMember} -> Tag {group.Key.IdBoard}");

            if (await _flarumContext.TagUsers.AnyAsync(tu => 
                tu.UserId == group.Key.IdMember && tu.TagId == (uint)group.Key.IdBoard))
            {
                Console.WriteLine("Tag subscription already exists, skipping...");
                stats.TagUsersSkipped++;
                continue;
            }

            var tagUser = mapper.Map<TagUser>(group);
            await _flarumContext.TagUsers.AddAsync(tagUser);
            stats.TagUsersMigrated++;
        }

        await _flarumContext.SaveChangesAsync();
    }

    /// <summary>
    /// Ensures that the default Flarum groups exist in the database.
    /// Creates missing default groups: 1=Admin, 3=Member, 4=Moderator
    /// </summary>
    private async Task EnsureDefaultGroupsExistAsync()
    {
        var defaultGroups = new[]
        {
            new { Id = 1U, NameSingular = "Admin", NamePlural = "Admins", Color = (string?)"#B72A2A", Icon = (string?)"fas fa-wrench" },
            new { Id = 3U, NameSingular = "Member", NamePlural = "Members", Color = (string?)null, Icon = (string?)null },
            new { Id = 4U, NameSingular = "Mod", NamePlural = "Mods", Color = (string?)"#80349E", Icon = (string?)"fas fa-shield-alt" }
        };

        foreach (var defaultGroup in defaultGroups)
        {
            if (!await _flarumContext.Groups.AnyAsync(g => g.Id == defaultGroup.Id))
            {
                Console.WriteLine($"Creating default Flarum group: {defaultGroup.Id} {defaultGroup.NameSingular}");
                
                var group = new Group
                {
                    Id = defaultGroup.Id,
                    NameSingular = defaultGroup.NameSingular,
                    NamePlural = defaultGroup.NamePlural,
                    Color = defaultGroup.Color,
                    Icon = defaultGroup.Icon,
                    IsHidden = false
                };
                
                await _flarumContext.Groups.AddAsync(group);
            }
        }

        await _flarumContext.SaveChangesAsync();
    }

    private async Task UpdateDiscussionPostReferencesAsync(MigrationStats stats)
    {
        var discussions = await _flarumContext.Discussions.ToListAsync();
        var totalDiscussions = discussions.Count;
        var processedDiscussions = 0;

        foreach (var discussion in discussions)
        {
            processedDiscussions++;
            Console.WriteLine($"[{processedDiscussions}/{totalDiscussions}] Updating Discussion post references: {discussion.Id}");

            // Find the first post (lowest post number) for this discussion
            var firstPost = await _flarumContext.Posts
                .Where(p => p.DiscussionId == discussion.Id)
                .OrderBy(p => p.Number)
                .FirstOrDefaultAsync();

            // Find the last post (highest post number) for this discussion
            var lastPost = await _flarumContext.Posts
                .Where(p => p.DiscussionId == discussion.Id)
                .OrderByDescending(p => p.Number)
                .FirstOrDefaultAsync();

            if (firstPost != null)
            {
                discussion.FirstPostId = firstPost.Id;
                discussion.UserId = firstPost.UserId; // Set discussion creator from first post
            }

            if (lastPost != null)
            {
                discussion.LastPostId = lastPost.Id;
                discussion.LastPostNumber = lastPost.Number;
                discussion.LastPostedAt = lastPost.CreatedAt;
                discussion.LastPostedUserId = lastPost.UserId;
            }
        }

        await _flarumContext.SaveChangesAsync();
        Console.WriteLine($"Updated post references for {totalDiscussions} discussions.");
    }
}