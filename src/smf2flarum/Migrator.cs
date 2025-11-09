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
}