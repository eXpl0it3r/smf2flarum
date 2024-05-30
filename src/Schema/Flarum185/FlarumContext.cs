using Microsoft.EntityFrameworkCore;

namespace Schema.Flarum185;

public partial class FlarumContext : DbContext
{
    public FlarumContext()
    {
    }

    public FlarumContext(DbContextOptions<FlarumContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessToken> AccessTokens { get; set; }

    public virtual DbSet<ApiKey> ApiKeys { get; set; }

    public virtual DbSet<Discussion> Discussions { get; set; }

    public virtual DbSet<DiscussionTag> DiscussionTags { get; set; }

    public virtual DbSet<DiscussionUser> DiscussionUsers { get; set; }

    public virtual DbSet<EmailToken> EmailTokens { get; set; }

    public virtual DbSet<Flag> Flags { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupPermission> GroupPermissions { get; set; }

    public virtual DbSet<GroupUser> GroupUsers { get; set; }

    public virtual DbSet<LoginProvider> LoginProviders { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PasswordToken> PasswordTokens { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostLike> PostLikes { get; set; }

    public virtual DbSet<PostMentionsGroup> PostMentionsGroups { get; set; }

    public virtual DbSet<PostMentionsPost> PostMentionsPosts { get; set; }

    public virtual DbSet<PostMentionsTag> PostMentionsTags { get; set; }

    public virtual DbSet<PostMentionsUser> PostMentionsUsers { get; set; }

    public virtual DbSet<RegistrationToken> RegistrationTokens { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagUser> TagUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("access_tokens");

            entity.HasIndex(e => e.Token, "access_tokens_token_unique").IsUnique();

            entity.HasIndex(e => e.Type, "access_tokens_type_index");

            entity.HasIndex(e => e.UserId, "access_tokens_user_id_foreign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.LastActivityAt)
                .HasColumnType("datetime")
                .HasColumnName("last_activity_at");
            entity.Property(e => e.LastIpAddress)
                .HasMaxLength(45)
                .HasColumnName("last_ip_address");
            entity.Property(e => e.LastUserAgent)
                .HasMaxLength(255)
                .HasColumnName("last_user_agent");
            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .HasColumnName("title");
            entity.Property(e => e.Token)
                .HasMaxLength(40)
                .HasColumnName("token");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AccessTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("access_tokens_user_id_foreign");
        });

        modelBuilder.Entity<ApiKey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("api_keys");

            entity.HasIndex(e => e.Key, "api_keys_key_unique").IsUnique();

            entity.HasIndex(e => e.UserId, "api_keys_user_id_foreign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AllowedIps)
                .HasMaxLength(255)
                .HasColumnName("allowed_ips");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Key)
                .HasMaxLength(100)
                .HasColumnName("key");
            entity.Property(e => e.LastActivityAt)
                .HasColumnType("datetime")
                .HasColumnName("last_activity_at");
            entity.Property(e => e.Scopes)
                .HasMaxLength(255)
                .HasColumnName("scopes");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ApiKeys)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("api_keys_user_id_foreign");
        });

        modelBuilder.Entity<Discussion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("discussions");

            entity.HasIndex(e => e.CommentCount, "discussions_comment_count_index");

            entity.HasIndex(e => e.CreatedAt, "discussions_created_at_index");

            entity.HasIndex(e => e.FirstPostId, "discussions_first_post_id_foreign");

            entity.HasIndex(e => e.HiddenAt, "discussions_hidden_at_index");

            entity.HasIndex(e => e.HiddenUserId, "discussions_hidden_user_id_foreign");

            entity.HasIndex(e => e.IsLocked, "discussions_is_locked_index");

            entity.HasIndex(e => new { e.IsSticky, e.CreatedAt }, "discussions_is_sticky_created_at_index");

            entity.HasIndex(e => new { e.IsSticky, e.LastPostedAt }, "discussions_is_sticky_last_posted_at_index");

            entity.HasIndex(e => e.LastPostId, "discussions_last_post_id_foreign");

            entity.HasIndex(e => e.LastPostedAt, "discussions_last_posted_at_index");

            entity.HasIndex(e => e.LastPostedUserId, "discussions_last_posted_user_id_index");

            entity.HasIndex(e => e.ParticipantCount, "discussions_participant_count_index");

            entity.HasIndex(e => e.UserId, "discussions_user_id_index");

            entity.HasIndex(e => e.Title, "title");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentCount)
                .HasDefaultValueSql("'1'")
                .HasColumnName("comment_count");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FirstPostId).HasColumnName("first_post_id");
            entity.Property(e => e.HiddenAt)
                .HasColumnType("datetime")
                .HasColumnName("hidden_at");
            entity.Property(e => e.HiddenUserId).HasColumnName("hidden_user_id");
            entity.Property(e => e.IsApproved)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_approved");
            entity.Property(e => e.IsLocked).HasColumnName("is_locked");
            entity.Property(e => e.IsPrivate).HasColumnName("is_private");
            entity.Property(e => e.IsSticky).HasColumnName("is_sticky");
            entity.Property(e => e.LastPostId).HasColumnName("last_post_id");
            entity.Property(e => e.LastPostNumber).HasColumnName("last_post_number");
            entity.Property(e => e.LastPostedAt)
                .HasColumnType("datetime")
                .HasColumnName("last_posted_at");
            entity.Property(e => e.LastPostedUserId).HasColumnName("last_posted_user_id");
            entity.Property(e => e.ParticipantCount).HasColumnName("participant_count");
            entity.Property(e => e.PostNumberIndex).HasColumnName("post_number_index");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.FirstPost).WithMany(p => p.DiscussionFirstPosts)
                .HasForeignKey(d => d.FirstPostId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("discussions_first_post_id_foreign");

            entity.HasOne(d => d.HiddenUser).WithMany(p => p.DiscussionHiddenUsers)
                .HasForeignKey(d => d.HiddenUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("discussions_hidden_user_id_foreign");

            entity.HasOne(d => d.LastPost).WithMany(p => p.DiscussionLastPosts)
                .HasForeignKey(d => d.LastPostId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("discussions_last_post_id_foreign");

            entity.HasOne(d => d.LastPostedUser).WithMany(p => p.DiscussionLastPostedUsers)
                .HasForeignKey(d => d.LastPostedUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("discussions_last_posted_user_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.DiscussionUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("discussions_user_id_foreign");
        });

        modelBuilder.Entity<DiscussionTag>(entity =>
        {
            entity.HasKey(e => new { e.DiscussionId, e.TagId }).HasName("PRIMARY");

            entity.ToTable("discussion_tag");

            entity.HasIndex(e => e.TagId, "discussion_tag_tag_id_foreign");

            entity.Property(e => e.DiscussionId).HasColumnName("discussion_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Discussion).WithMany(p => p.DiscussionTags)
                .HasForeignKey(d => d.DiscussionId)
                .HasConstraintName("discussion_tag_discussion_id_foreign");

            entity.HasOne(d => d.Tag).WithMany(p => p.DiscussionTags)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("discussion_tag_tag_id_foreign");
        });

        modelBuilder.Entity<DiscussionUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.DiscussionId }).HasName("PRIMARY");

            entity.ToTable("discussion_user");

            entity.HasIndex(e => e.DiscussionId, "discussion_user_discussion_id_foreign");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.DiscussionId).HasColumnName("discussion_id");
            entity.Property(e => e.LastReadAt)
                .HasColumnType("datetime")
                .HasColumnName("last_read_at");
            entity.Property(e => e.LastReadPostNumber).HasColumnName("last_read_post_number");
            entity.Property(e => e.Subscription)
                .HasColumnType("enum('follow','ignore')")
                .HasColumnName("subscription");

            entity.HasOne(d => d.Discussion).WithMany(p => p.DiscussionUsers)
                .HasForeignKey(d => d.DiscussionId)
                .HasConstraintName("discussion_user_discussion_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.DiscussionUsersNavigation)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("discussion_user_user_id_foreign");
        });

        modelBuilder.Entity<EmailToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("PRIMARY");

            entity.ToTable("email_tokens");

            entity.HasIndex(e => e.UserId, "email_tokens_user_id_foreign");

            entity.Property(e => e.Token)
                .HasMaxLength(100)
                .HasColumnName("token");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.EmailTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("email_tokens_user_id_foreign");
        });

        modelBuilder.Entity<Flag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("flags");

            entity.HasIndex(e => e.CreatedAt, "flags_created_at_index");

            entity.HasIndex(e => e.PostId, "flags_post_id_foreign");

            entity.HasIndex(e => e.UserId, "flags_user_id_foreign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasColumnName("reason");
            entity.Property(e => e.ReasonDetail)
                .HasColumnType("text")
                .HasColumnName("reason_detail");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Flags)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("flags_post_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.Flags)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("flags_user_id_foreign");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("groups");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .HasColumnName("icon");
            entity.Property(e => e.IsHidden).HasColumnName("is_hidden");
            entity.Property(e => e.NamePlural)
                .HasMaxLength(100)
                .HasColumnName("name_plural");
            entity.Property(e => e.NameSingular)
                .HasMaxLength(100)
                .HasColumnName("name_singular");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<GroupPermission>(entity =>
        {
            entity.HasKey(e => new { e.GroupId, e.Permission }).HasName("PRIMARY");

            entity.ToTable("group_permission");

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.Permission)
                .HasMaxLength(100)
                .HasColumnName("permission");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("group_permission_group_id_foreign");
        });

        modelBuilder.Entity<GroupUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.GroupId }).HasName("PRIMARY");

            entity.ToTable("group_user");

            entity.HasIndex(e => e.GroupId, "group_user_group_id_foreign");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupUsers)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("group_user_group_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.GroupUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("group_user_user_id_foreign");
        });

        modelBuilder.Entity<LoginProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("login_providers");

            entity.HasIndex(e => new { e.Provider, e.Identifier }, "login_providers_provider_identifier_unique").IsUnique();

            entity.HasIndex(e => e.UserId, "login_providers_user_id_foreign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Identifier)
                .HasMaxLength(100)
                .HasColumnName("identifier");
            entity.Property(e => e.LastLoginAt)
                .HasColumnType("datetime")
                .HasColumnName("last_login_at");
            entity.Property(e => e.Provider)
                .HasMaxLength(100)
                .HasColumnName("provider");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.LoginProviders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("login_providers_user_id_foreign");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("migrations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Extension)
                .HasMaxLength(255)
                .HasColumnName("extension");
            entity.Property(e => e.Migration1)
                .HasMaxLength(255)
                .HasColumnName("migration");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notifications");

            entity.HasIndex(e => e.FromUserId, "notifications_from_user_id_foreign");

            entity.HasIndex(e => e.UserId, "notifications_user_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Data)
                .HasColumnType("blob")
                .HasColumnName("data");
            entity.Property(e => e.FromUserId).HasColumnName("from_user_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ReadAt)
                .HasColumnType("datetime")
                .HasColumnName("read_at");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.FromUser).WithMany(p => p.NotificationFromUsers)
                .HasForeignKey(d => d.FromUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("notifications_from_user_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.NotificationUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("notifications_user_id_foreign");
        });

        modelBuilder.Entity<PasswordToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("PRIMARY");

            entity.ToTable("password_tokens");

            entity.HasIndex(e => e.UserId, "password_tokens_user_id_foreign");

            entity.Property(e => e.Token)
                .HasMaxLength(100)
                .HasColumnName("token");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.PasswordTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("password_tokens_user_id_foreign");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("posts");

            entity.HasIndex(e => e.Content, "content");

            entity.HasIndex(e => new { e.DiscussionId, e.CreatedAt }, "posts_discussion_id_created_at_index");

            entity.HasIndex(e => new { e.DiscussionId, e.Number }, "posts_discussion_id_number_index");

            entity.HasIndex(e => new { e.DiscussionId, e.Number }, "posts_discussion_id_number_unique").IsUnique();

            entity.HasIndex(e => e.EditedUserId, "posts_edited_user_id_foreign");

            entity.HasIndex(e => e.HiddenUserId, "posts_hidden_user_id_foreign");

            entity.HasIndex(e => new { e.Type, e.CreatedAt }, "posts_type_created_at_index");

            entity.HasIndex(e => e.Type, "posts_type_index");

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "posts_user_id_created_at_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasComment(" ")
                .HasColumnType("mediumtext")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscussionId).HasColumnName("discussion_id");
            entity.Property(e => e.EditedAt)
                .HasColumnType("datetime")
                .HasColumnName("edited_at");
            entity.Property(e => e.EditedUserId).HasColumnName("edited_user_id");
            entity.Property(e => e.HiddenAt)
                .HasColumnType("datetime")
                .HasColumnName("hidden_at");
            entity.Property(e => e.HiddenUserId).HasColumnName("hidden_user_id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.IsApproved)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_approved");
            entity.Property(e => e.IsPrivate).HasColumnName("is_private");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Discussion).WithMany(p => p.Posts)
                .HasForeignKey(d => d.DiscussionId)
                .HasConstraintName("posts_discussion_id_foreign");

            entity.HasOne(d => d.EditedUser).WithMany(p => p.PostEditedUsers)
                .HasForeignKey(d => d.EditedUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("posts_edited_user_id_foreign");

            entity.HasOne(d => d.HiddenUser).WithMany(p => p.PostHiddenUsers)
                .HasForeignKey(d => d.HiddenUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("posts_hidden_user_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.PostUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("posts_user_id_foreign");

            entity.HasMany(d => d.Users).WithMany(p => p.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "PostUser",
                    r => r.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("post_user_user_id_foreign"),
                    l => l.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .HasConstraintName("post_user_post_id_foreign"),
                    j =>
                    {
                        j.HasKey("PostId", "UserId").HasName("PRIMARY");
                        j.ToTable("post_user");
                        j.HasIndex(new[] { "UserId" }, "post_user_user_id_foreign");
                        j.IndexerProperty<uint>("PostId").HasColumnName("post_id");
                        j.IndexerProperty<uint>("UserId").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<PostLike>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.UserId }).HasName("PRIMARY");

            entity.ToTable("post_likes");

            entity.HasIndex(e => e.UserId, "post_likes_user_id_foreign");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Post).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("post_likes_post_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("post_likes_user_id_foreign");
        });

        modelBuilder.Entity<PostMentionsGroup>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.MentionsGroupId }).HasName("PRIMARY");

            entity.ToTable("post_mentions_group");

            entity.HasIndex(e => e.MentionsGroupId, "post_mentions_group_mentions_group_id_foreign");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.MentionsGroupId).HasColumnName("mentions_group_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.HasOne(d => d.MentionsGroup).WithMany(p => p.PostMentionsGroups)
                .HasForeignKey(d => d.MentionsGroupId)
                .HasConstraintName("post_mentions_group_mentions_group_id_foreign");

            entity.HasOne(d => d.Post).WithMany(p => p.PostMentionsGroups)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("post_mentions_group_post_id_foreign");
        });

        modelBuilder.Entity<PostMentionsPost>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.MentionsPostId }).HasName("PRIMARY");

            entity.ToTable("post_mentions_post");

            entity.HasIndex(e => e.MentionsPostId, "post_mentions_post_mentions_post_id_foreign");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.MentionsPostId).HasColumnName("mentions_post_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.HasOne(d => d.MentionsPost).WithMany(p => p.PostMentionsPostMentionsPosts)
                .HasForeignKey(d => d.MentionsPostId)
                .HasConstraintName("post_mentions_post_mentions_post_id_foreign");

            entity.HasOne(d => d.Post).WithMany(p => p.PostMentionsPostPosts)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("post_mentions_post_post_id_foreign");
        });

        modelBuilder.Entity<PostMentionsTag>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.MentionsTagId }).HasName("PRIMARY");

            entity.ToTable("post_mentions_tag");

            entity.HasIndex(e => e.MentionsTagId, "post_mentions_tag_mentions_tag_id_foreign");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.MentionsTagId).HasColumnName("mentions_tag_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.HasOne(d => d.MentionsTag).WithMany(p => p.PostMentionsTags)
                .HasForeignKey(d => d.MentionsTagId)
                .HasConstraintName("post_mentions_tag_mentions_tag_id_foreign");

            entity.HasOne(d => d.Post).WithMany(p => p.PostMentionsTags)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("post_mentions_tag_post_id_foreign");
        });

        modelBuilder.Entity<PostMentionsUser>(entity =>
        {
            entity.HasKey(e => new { e.PostId, e.MentionsUserId }).HasName("PRIMARY");

            entity.ToTable("post_mentions_user");

            entity.HasIndex(e => e.MentionsUserId, "post_mentions_user_mentions_user_id_foreign");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.MentionsUserId).HasColumnName("mentions_user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");

            entity.HasOne(d => d.MentionsUser).WithMany(p => p.PostMentionsUsers)
                .HasForeignKey(d => d.MentionsUserId)
                .HasConstraintName("post_mentions_user_mentions_user_id_foreign");

            entity.HasOne(d => d.Post).WithMany(p => p.PostMentionsUsers)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("post_mentions_user_post_id_foreign");
        });

        modelBuilder.Entity<RegistrationToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("PRIMARY");

            entity.ToTable("registration_tokens");

            entity.Property(e => e.Token)
                .HasMaxLength(100)
                .HasColumnName("token");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Identifier)
                .HasMaxLength(255)
                .HasColumnName("identifier");
            entity.Property(e => e.Payload)
                .HasColumnType("text")
                .HasColumnName("payload");
            entity.Property(e => e.Provider)
                .HasMaxLength(255)
                .HasColumnName("provider");
            entity.Property(e => e.UserAttributes)
                .HasColumnType("text")
                .HasColumnName("user_attributes");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Key).HasName("PRIMARY");

            entity.ToTable("settings");

            entity.Property(e => e.Key)
                .HasMaxLength(100)
                .HasColumnName("key");
            entity.Property(e => e.Value)
                .HasColumnType("text")
                .HasColumnName("value");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tags");

            entity.HasIndex(e => e.LastPostedDiscussionId, "tags_last_posted_discussion_id_foreign");

            entity.HasIndex(e => e.LastPostedUserId, "tags_last_posted_user_id_foreign");

            entity.HasIndex(e => e.ParentId, "tags_parent_id_foreign");

            entity.HasIndex(e => e.Slug, "tags_slug_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BackgroundMode)
                .HasMaxLength(100)
                .HasColumnName("background_mode");
            entity.Property(e => e.BackgroundPath)
                .HasMaxLength(100)
                .HasColumnName("background_path");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.DefaultSort)
                .HasMaxLength(50)
                .HasColumnName("default_sort");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DiscussionCount).HasColumnName("discussion_count");
            entity.Property(e => e.Icon)
                .HasMaxLength(100)
                .HasColumnName("icon");
            entity.Property(e => e.IsHidden).HasColumnName("is_hidden");
            entity.Property(e => e.IsRestricted).HasColumnName("is_restricted");
            entity.Property(e => e.LastPostedAt)
                .HasColumnType("datetime")
                .HasColumnName("last_posted_at");
            entity.Property(e => e.LastPostedDiscussionId).HasColumnName("last_posted_discussion_id");
            entity.Property(e => e.LastPostedUserId).HasColumnName("last_posted_user_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.LastPostedDiscussion).WithMany(p => p.Tags)
                .HasForeignKey(d => d.LastPostedDiscussionId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tags_last_posted_discussion_id_foreign");

            entity.HasOne(d => d.LastPostedUser).WithMany(p => p.Tags)
                .HasForeignKey(d => d.LastPostedUserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tags_last_posted_user_id_foreign");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("tags_parent_id_foreign");
        });

        modelBuilder.Entity<TagUser>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.TagId }).HasName("PRIMARY");

            entity.ToTable("tag_user");

            entity.HasIndex(e => e.TagId, "tag_user_tag_id_foreign");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.IsHidden).HasColumnName("is_hidden");
            entity.Property(e => e.MarkedAsReadAt)
                .HasColumnType("datetime")
                .HasColumnName("marked_as_read_at");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagUsers)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("tag_user_tag_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.TagUsers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("tag_user_user_id_foreign");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.CommentCount, "users_comment_count_index");

            entity.HasIndex(e => e.DiscussionCount, "users_discussion_count_index");

            entity.HasIndex(e => e.Email, "users_email_unique").IsUnique();

            entity.HasIndex(e => e.JoinedAt, "users_joined_at_index");

            entity.HasIndex(e => e.LastSeenAt, "users_last_seen_at_index");

            entity.HasIndex(e => e.Username, "users_username_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(100)
                .HasColumnName("avatar_url");
            entity.Property(e => e.CommentCount).HasColumnName("comment_count");
            entity.Property(e => e.DiscussionCount).HasColumnName("discussion_count");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.IsEmailConfirmed).HasColumnName("is_email_confirmed");
            entity.Property(e => e.JoinedAt)
                .HasColumnType("datetime")
                .HasColumnName("joined_at");
            entity.Property(e => e.LastSeenAt)
                .HasColumnType("datetime")
                .HasColumnName("last_seen_at");
            entity.Property(e => e.MarkedAllAsReadAt)
                .HasColumnType("datetime")
                .HasColumnName("marked_all_as_read_at");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Preferences)
                .HasColumnType("blob")
                .HasColumnName("preferences");
            entity.Property(e => e.ReadFlagsAt)
                .HasColumnType("datetime")
                .HasColumnName("read_flags_at");
            entity.Property(e => e.ReadNotificationsAt)
                .HasColumnType("datetime")
                .HasColumnName("read_notifications_at");
            entity.Property(e => e.SuspendMessage)
                .HasColumnType("text")
                .HasColumnName("suspend_message");
            entity.Property(e => e.SuspendReason)
                .HasColumnType("text")
                .HasColumnName("suspend_reason");
            entity.Property(e => e.SuspendedUntil)
                .HasColumnType("datetime")
                .HasColumnName("suspended_until");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
