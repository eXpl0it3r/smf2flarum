using Microsoft.EntityFrameworkCore;

namespace Schema.Smf2019;

public partial class SmfContext : DbContext
{
    public SmfContext()
    {
    }

    public SmfContext(DbContextOptions<SmfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminInfoFile> AdminInfoFiles { get; set; }

    public virtual DbSet<ApprovalQueue> ApprovalQueues { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<BanGroup> BanGroups { get; set; }

    public virtual DbSet<BanItem> BanItems { get; set; }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<BoardPermission> BoardPermissions { get; set; }

    public virtual DbSet<Calendar> Calendars { get; set; }

    public virtual DbSet<CalendarHoliday> CalendarHolidays { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CollapsedCategory> CollapsedCategories { get; set; }

    public virtual DbSet<CustomField> CustomFields { get; set; }

    public virtual DbSet<GroupModerator> GroupModerators { get; set; }

    public virtual DbSet<LogAction> LogActions { get; set; }

    public virtual DbSet<LogActivity> LogActivities { get; set; }

    public virtual DbSet<LogBanned> LogBanneds { get; set; }

    public virtual DbSet<LogBoard> LogBoards { get; set; }

    public virtual DbSet<LogComment> LogComments { get; set; }

    public virtual DbSet<LogDigest> LogDigests { get; set; }

    public virtual DbSet<LogError> LogErrors { get; set; }

    public virtual DbSet<LogFloodcontrol> LogFloodcontrols { get; set; }

    public virtual DbSet<LogGroupRequest> LogGroupRequests { get; set; }

    public virtual DbSet<LogKarma> LogKarmas { get; set; }

    public virtual DbSet<LogMarkRead> LogMarkReads { get; set; }

    public virtual DbSet<LogMemberNotice> LogMemberNotices { get; set; }

    public virtual DbSet<LogNotify> LogNotifies { get; set; }

    public virtual DbSet<LogOnline> LogOnlines { get; set; }

    public virtual DbSet<LogPackage> LogPackages { get; set; }

    public virtual DbSet<LogPoll> LogPolls { get; set; }

    public virtual DbSet<LogReported> LogReporteds { get; set; }

    public virtual DbSet<LogReportedComment> LogReportedComments { get; set; }

    public virtual DbSet<LogScheduledTask> LogScheduledTasks { get; set; }

    public virtual DbSet<LogSearchMessage> LogSearchMessages { get; set; }

    public virtual DbSet<LogSearchResult> LogSearchResults { get; set; }

    public virtual DbSet<LogSearchSubject> LogSearchSubjects { get; set; }

    public virtual DbSet<LogSearchTopic> LogSearchTopics { get; set; }

    public virtual DbSet<LogSpiderHit> LogSpiderHits { get; set; }

    public virtual DbSet<LogSpiderStat> LogSpiderStats { get; set; }

    public virtual DbSet<LogSubscribed> LogSubscribeds { get; set; }

    public virtual DbSet<LogTopic> LogTopics { get; set; }

    public virtual DbSet<MailQueue> MailQueues { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Membergroup> Membergroups { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageIcon> MessageIcons { get; set; }

    public virtual DbSet<Moderator> Moderators { get; set; }

    public virtual DbSet<OpenidAssoc> OpenidAssocs { get; set; }

    public virtual DbSet<PackageServer> PackageServers { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<PermissionProfile> PermissionProfiles { get; set; }

    public virtual DbSet<PersonalMessage> PersonalMessages { get; set; }

    public virtual DbSet<PmRecipient> PmRecipients { get; set; }

    public virtual DbSet<PmRule> PmRules { get; set; }

    public virtual DbSet<Poll> Polls { get; set; }

    public virtual DbSet<PollChoice> PollChoices { get; set; }

    public virtual DbSet<ScheduledTask> ScheduledTasks { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<Smiley> Smileys { get; set; }

    public virtual DbSet<Spider> Spiders { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<Theme> Themes { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminInfoFile>(entity =>
        {
            entity.HasKey(e => e.IdFile).HasName("PRIMARY");

            entity.ToTable("admin_info_files");

            entity.HasIndex(e => e.Filename, "filename");

            entity.Property(e => e.IdFile)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_file");
            entity.Property(e => e.Data)
                .HasColumnType("text")
                .HasColumnName("data");
            entity.Property(e => e.Filename)
                .HasDefaultValueSql("''")
                .HasColumnName("filename");
            entity.Property(e => e.Filetype)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("filetype");
            entity.Property(e => e.Parameters)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("parameters");
            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("path");
        });

        modelBuilder.Entity<ApprovalQueue>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("approval_queue");

            entity.Property(e => e.IdAttach).HasColumnName("id_attach");
            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.IdAttach).HasName("PRIMARY");

            entity.ToTable("attachments");

            entity.HasIndex(e => e.AttachmentType, "attachment_type");

            entity.HasIndex(e => new { e.IdMember, e.IdAttach }, "id_member").IsUnique();

            entity.HasIndex(e => e.IdMsg, "id_msg");

            entity.Property(e => e.IdAttach).HasColumnName("id_attach");
            entity.Property(e => e.Approved)
                .HasDefaultValueSql("'1'")
                .HasColumnName("approved");
            entity.Property(e => e.AttachmentType).HasColumnName("attachment_type");
            entity.Property(e => e.Downloads)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("downloads");
            entity.Property(e => e.FileHash)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("file_hash");
            entity.Property(e => e.Fileext)
                .HasMaxLength(8)
                .HasDefaultValueSql("''")
                .HasColumnName("fileext");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("filename");
            entity.Property(e => e.Height)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("height");
            entity.Property(e => e.IdFolder)
                .HasDefaultValueSql("'1'")
                .HasColumnName("id_folder");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
            entity.Property(e => e.IdThumb).HasColumnName("id_thumb");
            entity.Property(e => e.MimeType)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("mime_type");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.Width)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("width");
        });

        modelBuilder.Entity<BanGroup>(entity =>
        {
            entity.HasKey(e => e.IdBanGroup).HasName("PRIMARY");

            entity.ToTable("ban_groups");

            entity.Property(e => e.IdBanGroup)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_ban_group");
            entity.Property(e => e.BanTime).HasColumnName("ban_time");
            entity.Property(e => e.CannotAccess).HasColumnName("cannot_access");
            entity.Property(e => e.CannotLogin).HasColumnName("cannot_login");
            entity.Property(e => e.CannotPost).HasColumnName("cannot_post");
            entity.Property(e => e.CannotRegister).HasColumnName("cannot_register");
            entity.Property(e => e.ExpireTime).HasColumnName("expire_time");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("reason");
        });

        modelBuilder.Entity<BanItem>(entity =>
        {
            entity.HasKey(e => e.IdBan).HasName("PRIMARY");

            entity.ToTable("ban_items");

            entity.HasIndex(e => e.IdBanGroup, "id_ban_group");

            entity.Property(e => e.IdBan)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_ban");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("email_address");
            entity.Property(e => e.Hits)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("hits");
            entity.Property(e => e.Hostname)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("hostname");
            entity.Property(e => e.IdBanGroup).HasColumnName("id_ban_group");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IpHigh1).HasColumnName("ip_high1");
            entity.Property(e => e.IpHigh2).HasColumnName("ip_high2");
            entity.Property(e => e.IpHigh3).HasColumnName("ip_high3");
            entity.Property(e => e.IpHigh4).HasColumnName("ip_high4");
            entity.Property(e => e.IpLow1).HasColumnName("ip_low1");
            entity.Property(e => e.IpLow2).HasColumnName("ip_low2");
            entity.Property(e => e.IpLow3).HasColumnName("ip_low3");
            entity.Property(e => e.IpLow4).HasColumnName("ip_low4");
        });

        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(e => e.IdBoard).HasName("PRIMARY");

            entity.ToTable("boards");

            entity.HasIndex(e => new { e.IdCat, e.IdBoard }, "categories").IsUnique();

            entity.HasIndex(e => e.IdMsgUpdated, "id_msg_updated");

            entity.HasIndex(e => e.IdParent, "id_parent");

            entity.HasIndex(e => e.MemberGroups, "member_groups");

            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.BoardOrder).HasColumnName("board_order");
            entity.Property(e => e.ChildLevel).HasColumnName("child_level");
            entity.Property(e => e.CountPosts).HasColumnName("count_posts");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IdCat).HasColumnName("id_cat");
            entity.Property(e => e.IdLastMsg).HasColumnName("id_last_msg");
            entity.Property(e => e.IdMsgUpdated).HasColumnName("id_msg_updated");
            entity.Property(e => e.IdParent).HasColumnName("id_parent");
            entity.Property(e => e.IdProfile)
                .HasDefaultValueSql("'1'")
                .HasColumnName("id_profile");
            entity.Property(e => e.IdTheme).HasColumnName("id_theme");
            entity.Property(e => e.MemberGroups)
                .HasDefaultValueSql("'-1,0'")
                .HasColumnName("member_groups");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
            entity.Property(e => e.NumPosts)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("num_posts");
            entity.Property(e => e.NumTopics)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("num_topics");
            entity.Property(e => e.OverrideTheme).HasColumnName("override_theme");
            entity.Property(e => e.Redirect)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("redirect");
            entity.Property(e => e.UnapprovedPosts).HasColumnName("unapproved_posts");
            entity.Property(e => e.UnapprovedTopics).HasColumnName("unapproved_topics");
        });

        modelBuilder.Entity<BoardPermission>(entity =>
        {
            entity.HasKey(e => new { e.IdGroup, e.IdProfile, e.Permission }).HasName("PRIMARY");

            entity.ToTable("board_permissions");

            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.IdProfile).HasColumnName("id_profile");
            entity.Property(e => e.Permission)
                .HasMaxLength(30)
                .HasDefaultValueSql("''")
                .HasColumnName("permission");
            entity.Property(e => e.AddDeny)
                .HasDefaultValueSql("'1'")
                .HasColumnName("add_deny");
        });

        modelBuilder.Entity<Calendar>(entity =>
        {
            entity.HasKey(e => e.IdEvent).HasName("PRIMARY");

            entity.ToTable("calendar");

            entity.HasIndex(e => e.EndDate, "end_date");

            entity.HasIndex(e => e.StartDate, "start_date");

            entity.HasIndex(e => new { e.IdTopic, e.IdMember }, "topic");

            entity.Property(e => e.IdEvent).HasColumnName("id_event");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("'0001-01-01'")
                .HasColumnType("date")
                .HasColumnName("end_date");
            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("'0001-01-01'")
                .HasColumnType("date")
                .HasColumnName("start_date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("title");
        });

        modelBuilder.Entity<CalendarHoliday>(entity =>
        {
            entity.HasKey(e => e.IdHoliday).HasName("PRIMARY");

            entity.ToTable("calendar_holidays");

            entity.HasIndex(e => e.EventDate, "event_date");

            entity.Property(e => e.IdHoliday).HasColumnName("id_holiday");
            entity.Property(e => e.EventDate)
                .HasDefaultValueSql("'0001-01-01'")
                .HasColumnType("date")
                .HasColumnName("event_date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCat).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.IdCat)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_cat");
            entity.Property(e => e.CanCollapse)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("can_collapse");
            entity.Property(e => e.CatOrder).HasColumnName("cat_order");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
        });

        modelBuilder.Entity<CollapsedCategory>(entity =>
        {
            entity.HasKey(e => new { e.IdCat, e.IdMember }).HasName("PRIMARY");

            entity.ToTable("collapsed_categories");

            entity.Property(e => e.IdCat).HasColumnName("id_cat");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
        });

        modelBuilder.Entity<CustomField>(entity =>
        {
            entity.HasKey(e => e.IdField).HasName("PRIMARY");

            entity.ToTable("custom_fields");

            entity.HasIndex(e => e.ColName, "col_name").IsUnique();

            entity.Property(e => e.IdField).HasColumnName("id_field");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.Bbc).HasColumnName("bbc");
            entity.Property(e => e.CanSearch).HasColumnName("can_search");
            entity.Property(e => e.ColName)
                .HasMaxLength(12)
                .HasDefaultValueSql("''")
                .HasColumnName("col_name");
            entity.Property(e => e.DefaultValue)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("default_value");
            entity.Property(e => e.Enclose)
                .HasColumnType("text")
                .HasColumnName("enclose");
            entity.Property(e => e.FieldDesc)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("field_desc");
            entity.Property(e => e.FieldLength)
                .HasDefaultValueSql("'255'")
                .HasColumnName("field_length");
            entity.Property(e => e.FieldName)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("field_name");
            entity.Property(e => e.FieldOptions)
                .HasColumnType("text")
                .HasColumnName("field_options");
            entity.Property(e => e.FieldType)
                .HasMaxLength(8)
                .HasDefaultValueSql("'text'")
                .HasColumnName("field_type");
            entity.Property(e => e.Mask)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("mask");
            entity.Property(e => e.Placement).HasColumnName("placement");
            entity.Property(e => e.Private).HasColumnName("private");
            entity.Property(e => e.ShowDisplay).HasColumnName("show_display");
            entity.Property(e => e.ShowProfile)
                .HasMaxLength(20)
                .HasDefaultValueSql("'forumprofile'")
                .HasColumnName("show_profile");
            entity.Property(e => e.ShowReg).HasColumnName("show_reg");
        });

        modelBuilder.Entity<GroupModerator>(entity =>
        {
            entity.HasKey(e => new { e.IdGroup, e.IdMember }).HasName("PRIMARY");

            entity.ToTable("group_moderators");

            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
        });

        modelBuilder.Entity<LogAction>(entity =>
        {
            entity.HasKey(e => e.IdAction).HasName("PRIMARY");

            entity.ToTable("log_actions");

            entity.HasIndex(e => e.IdBoard, "id_board");

            entity.HasIndex(e => e.IdLog, "id_log");

            entity.HasIndex(e => e.IdMember, "id_member");

            entity.HasIndex(e => e.IdMsg, "id_msg");

            entity.HasIndex(e => e.LogTime, "log_time");

            entity.Property(e => e.IdAction).HasColumnName("id_action");
            entity.Property(e => e.Action)
                .HasMaxLength(30)
                .HasDefaultValueSql("''")
                .HasColumnName("action");
            entity.Property(e => e.Extra)
                .HasColumnType("text")
                .HasColumnName("extra");
            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.IdLog)
                .HasDefaultValueSql("'1'")
                .HasColumnName("id_log");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
            entity.Property(e => e.IdTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
            entity.Property(e => e.Ip)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .IsFixedLength()
                .HasColumnName("ip");
            entity.Property(e => e.LogTime).HasColumnName("log_time");
        });

        modelBuilder.Entity<LogActivity>(entity =>
        {
            entity.HasKey(e => e.Date).HasName("PRIMARY");

            entity.ToTable("log_activity");

            entity.HasIndex(e => e.MostOn, "most_on");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("'0001-01-01'")
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Hits)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("hits");
            entity.Property(e => e.MostOn).HasColumnName("most_on");
            entity.Property(e => e.Posts).HasColumnName("posts");
            entity.Property(e => e.Registers).HasColumnName("registers");
            entity.Property(e => e.Topics).HasColumnName("topics");
        });

        modelBuilder.Entity<LogBanned>(entity =>
        {
            entity.HasKey(e => e.IdBanLog).HasName("PRIMARY");

            entity.ToTable("log_banned");

            entity.HasIndex(e => e.LogTime, "log_time");

            entity.Property(e => e.IdBanLog)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_ban_log");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("email");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.Ip)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .IsFixedLength()
                .HasColumnName("ip");
            entity.Property(e => e.LogTime).HasColumnName("log_time");
        });

        modelBuilder.Entity<LogBoard>(entity =>
        {
            entity.HasKey(e => new { e.IdMember, e.IdBoard }).HasName("PRIMARY");

            entity.ToTable("log_boards");

            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
        });

        modelBuilder.Entity<LogComment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PRIMARY");

            entity.ToTable("log_comments");

            entity.HasIndex(e => e.CommentType, "comment_type");

            entity.HasIndex(e => e.IdRecipient, "id_recipient");

            entity.HasIndex(e => e.LogTime, "log_time");

            entity.Property(e => e.IdComment)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_comment");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.CommentType)
                .HasMaxLength(8)
                .HasDefaultValueSql("'warning'")
                .HasColumnName("comment_type");
            entity.Property(e => e.Counter).HasColumnName("counter");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdNotice)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_notice");
            entity.Property(e => e.IdRecipient)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_recipient");
            entity.Property(e => e.LogTime).HasColumnName("log_time");
            entity.Property(e => e.MemberName)
                .HasMaxLength(80)
                .HasDefaultValueSql("''")
                .HasColumnName("member_name");
            entity.Property(e => e.RecipientName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("recipient_name");
        });

        modelBuilder.Entity<LogDigest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("log_digest");

            entity.Property(e => e.Daily).HasColumnName("daily");
            entity.Property(e => e.Exclude)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("exclude");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
            entity.Property(e => e.IdTopic)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
            entity.Property(e => e.NoteType)
                .HasMaxLength(10)
                .HasDefaultValueSql("'post'")
                .HasColumnName("note_type");
        });

        modelBuilder.Entity<LogError>(entity =>
        {
            entity.HasKey(e => e.IdError).HasName("PRIMARY");

            entity.ToTable("log_errors");

            entity.HasIndex(e => e.IdMember, "id_member");

            entity.HasIndex(e => e.Ip, "ip");

            entity.HasIndex(e => e.LogTime, "log_time");

            entity.Property(e => e.IdError)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_error");
            entity.Property(e => e.ErrorType)
                .HasMaxLength(15)
                .HasDefaultValueSql("'general'")
                .IsFixedLength()
                .HasColumnName("error_type");
            entity.Property(e => e.File)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("file");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.Ip)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .IsFixedLength()
                .HasColumnName("ip");
            entity.Property(e => e.Line)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("line");
            entity.Property(e => e.LogTime).HasColumnName("log_time");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.Session)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .IsFixedLength()
                .HasColumnName("session");
            entity.Property(e => e.Url)
                .HasColumnType("text")
                .HasColumnName("url");
        });

        modelBuilder.Entity<LogFloodcontrol>(entity =>
        {
            entity.HasKey(e => new { e.Ip, e.LogType }).HasName("PRIMARY");

            entity.ToTable("log_floodcontrol");

            entity.Property(e => e.Ip)
                .HasMaxLength(16)
                .HasDefaultValueSql("''")
                .IsFixedLength()
                .HasColumnName("ip");
            entity.Property(e => e.LogType)
                .HasMaxLength(8)
                .HasDefaultValueSql("'post'")
                .HasColumnName("log_type");
            entity.Property(e => e.LogTime).HasColumnName("log_time");
        });

        modelBuilder.Entity<LogGroupRequest>(entity =>
        {
            entity.HasKey(e => e.IdRequest).HasName("PRIMARY");

            entity.ToTable("log_group_requests");

            entity.HasIndex(e => new { e.IdMember, e.IdGroup }, "id_member").IsUnique();

            entity.Property(e => e.IdRequest)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_request");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.Reason)
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.TimeApplied).HasColumnName("time_applied");
        });

        modelBuilder.Entity<LogKarma>(entity =>
        {
            entity.HasKey(e => new { e.IdTarget, e.IdExecutor }).HasName("PRIMARY");

            entity.ToTable("log_karma");

            entity.HasIndex(e => e.LogTime, "log_time");

            entity.Property(e => e.IdTarget)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_target");
            entity.Property(e => e.IdExecutor)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_executor");
            entity.Property(e => e.Action).HasColumnName("action");
            entity.Property(e => e.LogTime).HasColumnName("log_time");
        });

        modelBuilder.Entity<LogMarkRead>(entity =>
        {
            entity.HasKey(e => new { e.IdMember, e.IdBoard }).HasName("PRIMARY");

            entity.ToTable("log_mark_read");

            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
        });

        modelBuilder.Entity<LogMemberNotice>(entity =>
        {
            entity.HasKey(e => e.IdNotice).HasName("PRIMARY");

            entity.ToTable("log_member_notices");

            entity.Property(e => e.IdNotice)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_notice");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("subject");
        });

        modelBuilder.Entity<LogNotify>(entity =>
        {
            entity.HasKey(e => new { e.IdMember, e.IdTopic, e.IdBoard }).HasName("PRIMARY");

            entity.ToTable("log_notify");

            entity.HasIndex(e => new { e.IdTopic, e.IdMember }, "id_topic");

            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.Sent).HasColumnName("sent");
        });

        modelBuilder.Entity<LogOnline>(entity =>
        {
            entity.HasKey(e => e.Session).HasName("PRIMARY");

            entity.ToTable("log_online");

            entity.HasIndex(e => e.IdMember, "id_member");

            entity.HasIndex(e => e.LogTime, "log_time");

            entity.Property(e => e.Session)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasColumnName("session");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdSpider).HasColumnName("id_spider");
            entity.Property(e => e.Ip).HasColumnName("ip");
            entity.Property(e => e.LogTime).HasColumnName("log_time");
            entity.Property(e => e.Url)
                .HasColumnType("text")
                .HasColumnName("url");
        });

        modelBuilder.Entity<LogPackage>(entity =>
        {
            entity.HasKey(e => e.IdInstall).HasName("PRIMARY");

            entity.ToTable("log_packages");

            entity.HasIndex(e => e.Filename, "filename");

            entity.Property(e => e.IdInstall).HasColumnName("id_install");
            entity.Property(e => e.DbChanges)
                .HasColumnType("text")
                .HasColumnName("db_changes");
            entity.Property(e => e.FailedSteps)
                .HasColumnType("text")
                .HasColumnName("failed_steps");
            entity.Property(e => e.Filename)
                .HasDefaultValueSql("''")
                .HasColumnName("filename");
            entity.Property(e => e.IdMemberInstalled)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint")
                .HasColumnName("id_member_installed");
            entity.Property(e => e.IdMemberRemoved)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint")
                .HasColumnName("id_member_removed");
            entity.Property(e => e.InstallState)
                .HasDefaultValueSql("'1'")
                .HasColumnName("install_state");
            entity.Property(e => e.MemberInstalled)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("member_installed");
            entity.Property(e => e.MemberRemoved)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("member_removed");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
            entity.Property(e => e.PackageId)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("package_id");
            entity.Property(e => e.ThemesInstalled)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("themes_installed");
            entity.Property(e => e.TimeInstalled).HasColumnName("time_installed");
            entity.Property(e => e.TimeRemoved).HasColumnName("time_removed");
            entity.Property(e => e.Version)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("version");
        });

        modelBuilder.Entity<LogPoll>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("log_polls");

            entity.HasIndex(e => new { e.IdPoll, e.IdMember, e.IdChoice }, "id_poll");

            entity.Property(e => e.IdChoice).HasColumnName("id_choice");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdPoll)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_poll");
        });

        modelBuilder.Entity<LogReported>(entity =>
        {
            entity.HasKey(e => e.IdReport).HasName("PRIMARY");

            entity.ToTable("log_reported");

            entity.HasIndex(e => e.Closed, "closed");

            entity.HasIndex(e => e.IdMember, "id_member");

            entity.HasIndex(e => e.IdMsg, "id_msg");

            entity.HasIndex(e => e.IdTopic, "id_topic");

            entity.HasIndex(e => e.TimeStarted, "time_started");

            entity.Property(e => e.IdReport)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_report");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.Closed).HasColumnName("closed");
            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
            entity.Property(e => e.IdTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
            entity.Property(e => e.IgnoreAll).HasColumnName("ignore_all");
            entity.Property(e => e.Membername)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("membername");
            entity.Property(e => e.NumReports)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint")
                .HasColumnName("num_reports");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("subject");
            entity.Property(e => e.TimeStarted).HasColumnName("time_started");
            entity.Property(e => e.TimeUpdated).HasColumnName("time_updated");
        });

        modelBuilder.Entity<LogReportedComment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PRIMARY");

            entity.ToTable("log_reported_comments");

            entity.HasIndex(e => e.IdMember, "id_member");

            entity.HasIndex(e => e.IdReport, "id_report");

            entity.HasIndex(e => e.TimeSent, "time_sent");

            entity.Property(e => e.IdComment)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_comment");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("comment");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("email_address");
            entity.Property(e => e.IdMember)
                .HasColumnType("mediumint")
                .HasColumnName("id_member");
            entity.Property(e => e.IdReport)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint")
                .HasColumnName("id_report");
            entity.Property(e => e.MemberIp)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("member_ip");
            entity.Property(e => e.Membername)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("membername");
            entity.Property(e => e.TimeSent).HasColumnName("time_sent");
        });

        modelBuilder.Entity<LogScheduledTask>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("PRIMARY");

            entity.ToTable("log_scheduled_tasks");

            entity.Property(e => e.IdLog)
                .HasColumnType("mediumint")
                .HasColumnName("id_log");
            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.TimeRun).HasColumnName("time_run");
            entity.Property(e => e.TimeTaken).HasColumnName("time_taken");
        });

        modelBuilder.Entity<LogSearchMessage>(entity =>
        {
            entity.HasKey(e => new { e.IdSearch, e.IdMsg }).HasName("PRIMARY");

            entity.ToTable("log_search_messages");

            entity.Property(e => e.IdSearch).HasColumnName("id_search");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
        });

        modelBuilder.Entity<LogSearchResult>(entity =>
        {
            entity.HasKey(e => new { e.IdSearch, e.IdTopic }).HasName("PRIMARY");

            entity.ToTable("log_search_results");

            entity.Property(e => e.IdSearch).HasColumnName("id_search");
            entity.Property(e => e.IdTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
            entity.Property(e => e.NumMatches).HasColumnName("num_matches");
            entity.Property(e => e.Relevance).HasColumnName("relevance");
        });

        modelBuilder.Entity<LogSearchSubject>(entity =>
        {
            entity.HasKey(e => new { e.Word, e.IdTopic }).HasName("PRIMARY");

            entity.ToTable("log_search_subjects");

            entity.HasIndex(e => e.IdTopic, "id_topic");

            entity.Property(e => e.Word)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("word");
            entity.Property(e => e.IdTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
        });

        modelBuilder.Entity<LogSearchTopic>(entity =>
        {
            entity.HasKey(e => new { e.IdSearch, e.IdTopic }).HasName("PRIMARY");

            entity.ToTable("log_search_topics");

            entity.Property(e => e.IdSearch).HasColumnName("id_search");
            entity.Property(e => e.IdTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
        });

        modelBuilder.Entity<LogSpiderHit>(entity =>
        {
            entity.HasKey(e => e.IdHit).HasName("PRIMARY");

            entity.ToTable("log_spider_hits");

            entity.HasIndex(e => e.IdSpider, "id_spider");

            entity.HasIndex(e => e.LogTime, "log_time");

            entity.HasIndex(e => e.Processed, "processed");

            entity.Property(e => e.IdHit).HasColumnName("id_hit");
            entity.Property(e => e.IdSpider).HasColumnName("id_spider");
            entity.Property(e => e.LogTime).HasColumnName("log_time");
            entity.Property(e => e.Processed).HasColumnName("processed");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("url");
        });

        modelBuilder.Entity<LogSpiderStat>(entity =>
        {
            entity.HasKey(e => new { e.StatDate, e.IdSpider }).HasName("PRIMARY");

            entity.ToTable("log_spider_stats");

            entity.Property(e => e.StatDate)
                .HasDefaultValueSql("'0001-01-01'")
                .HasColumnType("date")
                .HasColumnName("stat_date");
            entity.Property(e => e.IdSpider).HasColumnName("id_spider");
            entity.Property(e => e.LastSeen).HasColumnName("last_seen");
            entity.Property(e => e.PageHits).HasColumnName("page_hits");
        });

        modelBuilder.Entity<LogSubscribed>(entity =>
        {
            entity.HasKey(e => e.IdSublog).HasName("PRIMARY");

            entity.ToTable("log_subscribed");

            entity.HasIndex(e => e.EndTime, "end_time");

            entity.HasIndex(e => e.IdMember, "id_member");

            entity.HasIndex(e => new { e.IdSubscribe, e.IdMember }, "id_subscribe").IsUnique();

            entity.HasIndex(e => e.PaymentsPending, "payments_pending");

            entity.HasIndex(e => e.ReminderSent, "reminder_sent");

            entity.HasIndex(e => e.Status, "status");

            entity.Property(e => e.IdSublog).HasColumnName("id_sublog");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IdMember).HasColumnName("id_member");
            entity.Property(e => e.IdSubscribe)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_subscribe");
            entity.Property(e => e.OldIdGroup).HasColumnName("old_id_group");
            entity.Property(e => e.PaymentsPending).HasColumnName("payments_pending");
            entity.Property(e => e.PendingDetails)
                .HasColumnType("text")
                .HasColumnName("pending_details");
            entity.Property(e => e.ReminderSent).HasColumnName("reminder_sent");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.VendorRef)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("vendor_ref");
        });

        modelBuilder.Entity<LogTopic>(entity =>
        {
            entity.HasKey(e => new { e.IdMember, e.IdTopic }).HasName("PRIMARY");

            entity.ToTable("log_topics");

            entity.HasIndex(e => e.IdTopic, "id_topic");

            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
        });

        modelBuilder.Entity<MailQueue>(entity =>
        {
            entity.HasKey(e => e.IdMail).HasName("PRIMARY");

            entity.ToTable("mail_queue");

            entity.HasIndex(e => new { e.Priority, e.IdMail }, "mail_priority");

            entity.HasIndex(e => e.TimeSent, "time_sent");

            entity.Property(e => e.IdMail).HasColumnName("id_mail");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.Headers)
                .HasColumnType("text")
                .HasColumnName("headers");
            entity.Property(e => e.Priority)
                .HasDefaultValueSql("'1'")
                .HasColumnName("priority");
            entity.Property(e => e.Private).HasColumnName("private");
            entity.Property(e => e.Recipient)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("recipient");
            entity.Property(e => e.SendHtml).HasColumnName("send_html");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("subject");
            entity.Property(e => e.TimeSent).HasColumnName("time_sent");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.IdMember).HasName("PRIMARY");

            entity.ToTable("members");

            entity.HasIndex(e => e.Birthdate, "birthdate");

            entity.HasIndex(e => e.DateRegistered, "date_registered");

            entity.HasIndex(e => e.IdGroup, "id_group");

            entity.HasIndex(e => e.IdPostGroup, "id_post_group");

            entity.HasIndex(e => e.IdTheme, "id_theme");

            entity.HasIndex(e => e.LastLogin, "last_login");

            entity.HasIndex(e => e.Lngfile, "lngfile");

            entity.HasIndex(e => e.MemberName, "member_name");

            entity.HasIndex(e => e.Posts, "posts");

            entity.HasIndex(e => e.RealName, "real_name");

            entity.HasIndex(e => e.TotalTimeLoggedIn, "total_time_logged_in");

            entity.HasIndex(e => e.Warning, "warning");

            entity.Property(e => e.IdMember)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.AdditionalGroups)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("additional_groups");
            entity.Property(e => e.Aim)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("aim");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("avatar");
            entity.Property(e => e.Birthdate)
                .HasDefaultValueSql("'0001-01-01'")
                .HasColumnType("date")
                .HasColumnName("birthdate");
            entity.Property(e => e.BuddyList)
                .HasColumnType("text")
                .HasColumnName("buddy_list");
            entity.Property(e => e.DateRegistered).HasColumnName("date_registered");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("email_address");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HideEmail).HasColumnName("hide_email");
            entity.Property(e => e.Icq)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("icq");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.IdMsgLastVisit).HasColumnName("id_msg_last_visit");
            entity.Property(e => e.IdPostGroup).HasColumnName("id_post_group");
            entity.Property(e => e.IdTheme).HasColumnName("id_theme");
            entity.Property(e => e.IgnoreBoards)
                .HasColumnType("text")
                .HasColumnName("ignore_boards");
            entity.Property(e => e.InstantMessages).HasColumnName("instant_messages");
            entity.Property(e => e.IsActivated)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_activated");
            entity.Property(e => e.KarmaBad).HasColumnName("karma_bad");
            entity.Property(e => e.KarmaGood).HasColumnName("karma_good");
            entity.Property(e => e.LastLogin).HasColumnName("last_login");
            entity.Property(e => e.Lngfile)
                .HasDefaultValueSql("''")
                .HasColumnName("lngfile");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("location");
            entity.Property(e => e.MemberIp)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("member_ip");
            entity.Property(e => e.MemberIp2)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("member_ip2");
            entity.Property(e => e.MemberName)
                .HasMaxLength(80)
                .HasDefaultValueSql("''")
                .HasColumnName("member_name");
            entity.Property(e => e.MessageLabels)
                .HasColumnType("text")
                .HasColumnName("message_labels");
            entity.Property(e => e.ModPrefs)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("mod_prefs");
            entity.Property(e => e.Msn)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("msn");
            entity.Property(e => e.NewPm).HasColumnName("new_pm");
            entity.Property(e => e.NotifyAnnouncements)
                .HasDefaultValueSql("'1'")
                .HasColumnName("notify_announcements");
            entity.Property(e => e.NotifyRegularity)
                .HasDefaultValueSql("'1'")
                .HasColumnName("notify_regularity");
            entity.Property(e => e.NotifySendBody).HasColumnName("notify_send_body");
            entity.Property(e => e.NotifyTypes)
                .HasDefaultValueSql("'2'")
                .HasColumnName("notify_types");
            entity.Property(e => e.OpenidUri)
                .HasColumnType("text")
                .HasColumnName("openid_uri");
            entity.Property(e => e.Passwd)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("passwd");
            entity.Property(e => e.PasswdFlood)
                .HasMaxLength(12)
                .HasDefaultValueSql("''")
                .HasColumnName("passwd_flood");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("password_salt");
            entity.Property(e => e.PersonalText)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("personal_text");
            entity.Property(e => e.PmEmailNotify).HasColumnName("pm_email_notify");
            entity.Property(e => e.PmIgnoreList)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("pm_ignore_list");
            entity.Property(e => e.PmPrefs)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint")
                .HasColumnName("pm_prefs");
            entity.Property(e => e.PmReceiveFrom)
                .HasDefaultValueSql("'1'")
                .HasColumnName("pm_receive_from");
            entity.Property(e => e.Posts)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("posts");
            entity.Property(e => e.RealName)
                .HasDefaultValueSql("''")
                .HasColumnName("real_name");
            entity.Property(e => e.SecretAnswer)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("secret_answer");
            entity.Property(e => e.SecretQuestion)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("secret_question");
            entity.Property(e => e.ShowOnline)
                .HasDefaultValueSql("'1'")
                .HasColumnName("show_online");
            entity.Property(e => e.Signature)
                .HasColumnType("text")
                .HasColumnName("signature");
            entity.Property(e => e.SmileySet)
                .HasMaxLength(48)
                .HasDefaultValueSql("''")
                .HasColumnName("smiley_set");
            entity.Property(e => e.TimeFormat)
                .HasMaxLength(80)
                .HasDefaultValueSql("''")
                .HasColumnName("time_format");
            entity.Property(e => e.TimeOffset).HasColumnName("time_offset");
            entity.Property(e => e.TotalTimeLoggedIn).HasColumnName("total_time_logged_in");
            entity.Property(e => e.UnreadMessages).HasColumnName("unread_messages");
            entity.Property(e => e.Usertitle)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("usertitle");
            entity.Property(e => e.ValidationCode)
                .HasMaxLength(10)
                .HasDefaultValueSql("''")
                .HasColumnName("validation_code");
            entity.Property(e => e.Warning).HasColumnName("warning");
            entity.Property(e => e.WebsiteTitle)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("website_title");
            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("website_url");
            entity.Property(e => e.Yim)
                .HasMaxLength(32)
                .HasDefaultValueSql("''")
                .HasColumnName("yim");
        });

        modelBuilder.Entity<Membergroup>(entity =>
        {
            entity.HasKey(e => e.IdGroup).HasName("PRIMARY");

            entity.ToTable("membergroups");

            entity.HasIndex(e => e.MinPosts, "min_posts");

            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.GroupName)
                .HasMaxLength(80)
                .HasDefaultValueSql("''")
                .HasColumnName("group_name");
            entity.Property(e => e.GroupType).HasColumnName("group_type");
            entity.Property(e => e.Hidden).HasColumnName("hidden");
            entity.Property(e => e.IdParent)
                .HasDefaultValueSql("'-2'")
                .HasColumnName("id_parent");
            entity.Property(e => e.MaxMessages).HasColumnName("max_messages");
            entity.Property(e => e.MinPosts)
                .HasDefaultValueSql("'-1'")
                .HasColumnType("mediumint")
                .HasColumnName("min_posts");
            entity.Property(e => e.OnlineColor)
                .HasMaxLength(20)
                .HasDefaultValueSql("''")
                .HasColumnName("online_color");
            entity.Property(e => e.Stars)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("stars");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.IdMsg).HasName("PRIMARY");

            entity.ToTable("messages");

            entity.HasIndex(e => e.Approved, "approved");

            entity.HasIndex(e => new { e.IdTopic, e.IdMsg, e.IdMember, e.Approved }, "current_topic");

            entity.HasIndex(e => new { e.IdBoard, e.IdMsg }, "id_board").IsUnique();

            entity.HasIndex(e => new { e.IdMember, e.IdMsg }, "id_member").IsUnique();

            entity.HasIndex(e => new { e.IdMember, e.Approved, e.IdMsg }, "id_member_msg");

            entity.HasIndex(e => e.IdTopic, "id_topic");

            entity.HasIndex(e => new { e.PosterIp, e.IdTopic }, "ip_index");

            entity.HasIndex(e => new { e.IdMember, e.IdTopic }, "participation");

            entity.HasIndex(e => new { e.IdMember, e.PosterIp, e.IdMsg }, "related_ip");

            entity.HasIndex(e => new { e.IdMember, e.IdBoard }, "show_posts");

            entity.HasIndex(e => new { e.IdTopic, e.IdMsg }, "topic").IsUnique();

            entity.Property(e => e.IdMsg).HasColumnName("id_msg");
            entity.Property(e => e.Approved)
                .HasDefaultValueSql("'1'")
                .HasColumnName("approved");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.Icon)
                .HasMaxLength(16)
                .HasDefaultValueSql("'xx'")
                .HasColumnName("icon");
            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.IdMsgModified).HasColumnName("id_msg_modified");
            entity.Property(e => e.IdTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
            entity.Property(e => e.ModifiedName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("modified_name");
            entity.Property(e => e.ModifiedTime).HasColumnName("modified_time");
            entity.Property(e => e.PosterEmail)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("poster_email");
            entity.Property(e => e.PosterIp)
                .HasDefaultValueSql("''")
                .HasColumnName("poster_ip");
            entity.Property(e => e.PosterName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("poster_name");
            entity.Property(e => e.PosterTime).HasColumnName("poster_time");
            entity.Property(e => e.SmileysEnabled)
                .HasDefaultValueSql("'1'")
                .HasColumnName("smileys_enabled");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("subject");
        });

        modelBuilder.Entity<MessageIcon>(entity =>
        {
            entity.HasKey(e => e.IdIcon).HasName("PRIMARY");

            entity.ToTable("message_icons");

            entity.HasIndex(e => e.IdBoard, "id_board");

            entity.Property(e => e.IdIcon).HasColumnName("id_icon");
            entity.Property(e => e.Filename)
                .HasMaxLength(80)
                .HasDefaultValueSql("''")
                .HasColumnName("filename");
            entity.Property(e => e.IconOrder).HasColumnName("icon_order");
            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.Title)
                .HasMaxLength(80)
                .HasDefaultValueSql("''")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Moderator>(entity =>
        {
            entity.HasKey(e => new { e.IdBoard, e.IdMember }).HasName("PRIMARY");

            entity.ToTable("moderators");

            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
        });

        modelBuilder.Entity<OpenidAssoc>(entity =>
        {
            entity.HasKey(e => new { e.ServerUrl, e.Handle }).HasName("PRIMARY");

            entity.ToTable("openid_assoc");

            entity.HasIndex(e => e.Expires, "expires");

            entity.Property(e => e.ServerUrl)
                .HasColumnType("text")
                .HasColumnName("server_url");
            entity.Property(e => e.Handle)
                .HasDefaultValueSql("''")
                .HasColumnName("handle");
            entity.Property(e => e.AssocType)
                .HasMaxLength(64)
                .HasColumnName("assoc_type");
            entity.Property(e => e.Expires).HasColumnName("expires");
            entity.Property(e => e.Issued).HasColumnName("issued");
            entity.Property(e => e.Secret)
                .HasColumnType("text")
                .HasColumnName("secret");
        });

        modelBuilder.Entity<PackageServer>(entity =>
        {
            entity.HasKey(e => e.IdServer).HasName("PRIMARY");

            entity.ToTable("package_servers");

            entity.Property(e => e.IdServer).HasColumnName("id_server");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("url");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => new { e.IdGroup, e.Permission1 }).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.Permission1)
                .HasMaxLength(30)
                .HasDefaultValueSql("''")
                .HasColumnName("permission");
            entity.Property(e => e.AddDeny)
                .HasDefaultValueSql("'1'")
                .HasColumnName("add_deny");
        });

        modelBuilder.Entity<PermissionProfile>(entity =>
        {
            entity.HasKey(e => e.IdProfile).HasName("PRIMARY");

            entity.ToTable("permission_profiles");

            entity.Property(e => e.IdProfile).HasColumnName("id_profile");
            entity.Property(e => e.ProfileName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("profile_name");
        });

        modelBuilder.Entity<PersonalMessage>(entity =>
        {
            entity.HasKey(e => e.IdPm).HasName("PRIMARY");

            entity.ToTable("personal_messages");

            entity.HasIndex(e => new { e.IdMemberFrom, e.DeletedBySender }, "id_member");

            entity.HasIndex(e => e.IdPmHead, "id_pm_head");

            entity.HasIndex(e => e.Msgtime, "msgtime");

            entity.Property(e => e.IdPm).HasColumnName("id_pm");
            entity.Property(e => e.Body)
                .HasColumnType("text")
                .HasColumnName("body");
            entity.Property(e => e.DeletedBySender).HasColumnName("deleted_by_sender");
            entity.Property(e => e.FromName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("from_name");
            entity.Property(e => e.IdMemberFrom)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member_from");
            entity.Property(e => e.IdPmHead).HasColumnName("id_pm_head");
            entity.Property(e => e.Msgtime).HasColumnName("msgtime");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("subject");
        });

        modelBuilder.Entity<PmRecipient>(entity =>
        {
            entity.HasKey(e => new { e.IdPm, e.IdMember }).HasName("PRIMARY");

            entity.ToTable("pm_recipients");

            entity.HasIndex(e => new { e.IdMember, e.Deleted, e.IdPm }, "id_member").IsUnique();

            entity.Property(e => e.IdPm).HasColumnName("id_pm");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member");
            entity.Property(e => e.Bcc).HasColumnName("bcc");
            entity.Property(e => e.Deleted).HasColumnName("deleted");
            entity.Property(e => e.IsNew).HasColumnName("is_new");
            entity.Property(e => e.IsRead).HasColumnName("is_read");
            entity.Property(e => e.Labels)
                .HasMaxLength(60)
                .HasDefaultValueSql("'-1'")
                .HasColumnName("labels");
        });

        modelBuilder.Entity<PmRule>(entity =>
        {
            entity.HasKey(e => e.IdRule).HasName("PRIMARY");

            entity.ToTable("pm_rules");

            entity.HasIndex(e => e.DeletePm, "delete_pm");

            entity.HasIndex(e => e.IdMember, "id_member");

            entity.Property(e => e.IdRule).HasColumnName("id_rule");
            entity.Property(e => e.Actions)
                .HasColumnType("text")
                .HasColumnName("actions");
            entity.Property(e => e.Criteria)
                .HasColumnType("text")
                .HasColumnName("criteria");
            entity.Property(e => e.DeletePm).HasColumnName("delete_pm");
            entity.Property(e => e.IdMember).HasColumnName("id_member");
            entity.Property(e => e.IsOr).HasColumnName("is_or");
            entity.Property(e => e.RuleName)
                .HasMaxLength(60)
                .HasColumnName("rule_name");
        });

        modelBuilder.Entity<Poll>(entity =>
        {
            entity.HasKey(e => e.IdPoll).HasName("PRIMARY");

            entity.ToTable("polls");

            entity.Property(e => e.IdPoll)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_poll");
            entity.Property(e => e.ChangeVote).HasColumnName("change_vote");
            entity.Property(e => e.ExpireTime).HasColumnName("expire_time");
            entity.Property(e => e.GuestVote).HasColumnName("guest_vote");
            entity.Property(e => e.HideResults).HasColumnName("hide_results");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint")
                .HasColumnName("id_member");
            entity.Property(e => e.MaxVotes)
                .HasDefaultValueSql("'1'")
                .HasColumnName("max_votes");
            entity.Property(e => e.NumGuestVoters).HasColumnName("num_guest_voters");
            entity.Property(e => e.PosterName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("poster_name");
            entity.Property(e => e.Question)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("question");
            entity.Property(e => e.ResetPoll).HasColumnName("reset_poll");
            entity.Property(e => e.VotingLocked).HasColumnName("voting_locked");
        });

        modelBuilder.Entity<PollChoice>(entity =>
        {
            entity.HasKey(e => new { e.IdPoll, e.IdChoice }).HasName("PRIMARY");

            entity.ToTable("poll_choices");

            entity.Property(e => e.IdPoll)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_poll");
            entity.Property(e => e.IdChoice).HasColumnName("id_choice");
            entity.Property(e => e.Label)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("label");
            entity.Property(e => e.Votes).HasColumnName("votes");
        });

        modelBuilder.Entity<ScheduledTask>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("PRIMARY");

            entity.ToTable("scheduled_tasks");

            entity.HasIndex(e => e.Disabled, "disabled");

            entity.HasIndex(e => e.NextTime, "next_time");

            entity.HasIndex(e => e.Task, "task").IsUnique();

            entity.Property(e => e.IdTask).HasColumnName("id_task");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.NextTime).HasColumnName("next_time");
            entity.Property(e => e.Task)
                .HasMaxLength(24)
                .HasDefaultValueSql("''")
                .HasColumnName("task");
            entity.Property(e => e.TimeOffset).HasColumnName("time_offset");
            entity.Property(e => e.TimeRegularity).HasColumnName("time_regularity");
            entity.Property(e => e.TimeUnit)
                .HasMaxLength(1)
                .HasDefaultValueSql("'h'")
                .HasColumnName("time_unit");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.SessionId).HasName("PRIMARY");

            entity.ToTable("sessions");

            entity.Property(e => e.SessionId)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("session_id");
            entity.Property(e => e.Data)
                .HasColumnType("text")
                .HasColumnName("data");
            entity.Property(e => e.LastUpdate).HasColumnName("last_update");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Variable).HasName("PRIMARY");

            entity.ToTable("settings");

            entity.Property(e => e.Variable)
                .HasDefaultValueSql("''")
                .HasColumnName("variable");
            entity.Property(e => e.Value)
                .HasColumnType("text")
                .HasColumnName("value");
        });

        modelBuilder.Entity<Smiley>(entity =>
        {
            entity.HasKey(e => e.IdSmiley).HasName("PRIMARY");

            entity.ToTable("smileys");

            entity.Property(e => e.IdSmiley).HasColumnName("id_smiley");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasDefaultValueSql("''")
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(80)
                .HasDefaultValueSql("''")
                .HasColumnName("description");
            entity.Property(e => e.Filename)
                .HasMaxLength(48)
                .HasDefaultValueSql("''")
                .HasColumnName("filename");
            entity.Property(e => e.Hidden).HasColumnName("hidden");
            entity.Property(e => e.SmileyOrder).HasColumnName("smiley_order");
            entity.Property(e => e.SmileyRow).HasColumnName("smiley_row");
        });

        modelBuilder.Entity<Spider>(entity =>
        {
            entity.HasKey(e => e.IdSpider).HasName("PRIMARY");

            entity.ToTable("spiders");

            entity.Property(e => e.IdSpider).HasColumnName("id_spider");
            entity.Property(e => e.IpInfo)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("ip_info");
            entity.Property(e => e.SpiderName)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("spider_name");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("user_agent");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.IdSubscribe).HasName("PRIMARY");

            entity.ToTable("subscriptions");

            entity.HasIndex(e => e.Active, "active");

            entity.Property(e => e.IdSubscribe)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_subscribe");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("'1'")
                .HasColumnName("active");
            entity.Property(e => e.AddGroups)
                .HasMaxLength(40)
                .HasDefaultValueSql("''")
                .HasColumnName("add_groups");
            entity.Property(e => e.AllowPartial).HasColumnName("allow_partial");
            entity.Property(e => e.Cost)
                .HasColumnType("text")
                .HasColumnName("cost");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasDefaultValueSql("''")
                .HasColumnName("description");
            entity.Property(e => e.EmailComplete)
                .HasColumnType("text")
                .HasColumnName("email_complete");
            entity.Property(e => e.IdGroup).HasColumnName("id_group");
            entity.Property(e => e.Length)
                .HasMaxLength(6)
                .HasDefaultValueSql("''")
                .HasColumnName("length");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasDefaultValueSql("''")
                .HasColumnName("name");
            entity.Property(e => e.Reminder).HasColumnName("reminder");
            entity.Property(e => e.Repeatable).HasColumnName("repeatable");
        });

        modelBuilder.Entity<Theme>(entity =>
        {
            entity.HasKey(e => new { e.IdTheme, e.IdMember, e.Variable }).HasName("PRIMARY");

            entity.ToTable("themes");

            entity.HasIndex(e => e.IdMember, "id_member");

            entity.Property(e => e.IdTheme)
                .HasDefaultValueSql("'1'")
                .HasColumnName("id_theme");
            entity.Property(e => e.IdMember)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint")
                .HasColumnName("id_member");
            entity.Property(e => e.Variable)
                .HasDefaultValueSql("''")
                .HasColumnName("variable");
            entity.Property(e => e.Value)
                .HasColumnType("text")
                .HasColumnName("value");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.IdTopic).HasName("PRIMARY");

            entity.ToTable("topics");

            entity.HasIndex(e => e.Approved, "approved");

            entity.HasIndex(e => new { e.IdBoard, e.IdFirstMsg }, "board_news");

            entity.HasIndex(e => new { e.IdFirstMsg, e.IdBoard }, "first_message").IsUnique();

            entity.HasIndex(e => e.IdBoard, "id_board");

            entity.HasIndex(e => e.IsSticky, "is_sticky");

            entity.HasIndex(e => new { e.IdLastMsg, e.IdBoard }, "last_message").IsUnique();

            entity.HasIndex(e => new { e.IdBoard, e.IsSticky, e.IdLastMsg }, "last_message_sticky");

            entity.HasIndex(e => new { e.IdMemberStarted, e.IdBoard }, "member_started");

            entity.HasIndex(e => new { e.IdPoll, e.IdTopic }, "poll").IsUnique();

            entity.Property(e => e.IdTopic)
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_topic");
            entity.Property(e => e.Approved)
                .HasDefaultValueSql("'1'")
                .HasColumnName("approved");
            entity.Property(e => e.IdBoard).HasColumnName("id_board");
            entity.Property(e => e.IdFirstMsg).HasColumnName("id_first_msg");
            entity.Property(e => e.IdLastMsg).HasColumnName("id_last_msg");
            entity.Property(e => e.IdMemberStarted)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member_started");
            entity.Property(e => e.IdMemberUpdated)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_member_updated");
            entity.Property(e => e.IdPoll)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint unsigned")
                .HasColumnName("id_poll");
            entity.Property(e => e.IdPreviousBoard).HasColumnName("id_previous_board");
            entity.Property(e => e.IdPreviousTopic)
                .HasDefaultValueSql("'0'")
                .HasColumnType("mediumint")
                .HasColumnName("id_previous_topic");
            entity.Property(e => e.IsSticky).HasColumnName("is_sticky");
            entity.Property(e => e.Locked).HasColumnName("locked");
            entity.Property(e => e.NumReplies).HasColumnName("num_replies");
            entity.Property(e => e.NumViews).HasColumnName("num_views");
            entity.Property(e => e.UnapprovedPosts).HasColumnName("unapproved_posts");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
