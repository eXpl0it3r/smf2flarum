namespace Mapping.Flarum185;

using AutoMapper;
using Schema.Smf2019;
using Schema.Flarum185;
using System.Text.RegularExpressions;

/// <summary>
/// AutoMapper profile for extracting mentions from SMF message content and creating Flarum mention entities.
/// Parses @username, #post, and other mention patterns from SMF message bodies.
/// </summary>
public class PostMentionsProfile : Profile
{
    public PostMentionsProfile()
    {
        // Create a custom converter that parses the message content for mentions
        CreateMap<Message, List<PostMentionsUser>>()
            .ConvertUsing((src, dest, context) => ExtractUserMentions(src, context));

        CreateMap<Message, List<PostMentionsPost>>()
            .ConvertUsing((src, dest, context) => ExtractPostMentions(src, context));

        CreateMap<Message, List<PostMentionsTag>>()
            .ConvertUsing((src, dest, context) => ExtractTagMentions(src, context));

        CreateMap<Message, List<PostMentionsGroup>>()
            .ConvertUsing((src, dest, context) => ExtractGroupMentions(src, context));
    }

    /// <summary>
    /// Extracts user mentions from message content (e.g., @username patterns).
    /// </summary>
    private static List<PostMentionsUser> ExtractUserMentions(Message message, ResolutionContext context)
    {
        var mentions = new List<PostMentionsUser>();
        
        if (string.IsNullOrEmpty(message.Body))
            return mentions;

        // Pattern for @username mentions (case-insensitive)
        var userMentionPattern = @"@([a-zA-Z0-9_\-\.]+)";
        var matches = Regex.Matches(message.Body, userMentionPattern, RegexOptions.IgnoreCase);

        var createdAt = Mapping.Converter.UnixTimeStampToDateTime(message.PosterTime) ?? DateTime.UtcNow;

        foreach (Match match in matches)
        {
            var username = match.Groups[1].Value;
            
            // In a real implementation, you'd look up the user ID from the username
            // For now, we'll create a placeholder that requires post-processing
            mentions.Add(new PostMentionsUser
            {
                PostId = message.IdMsg,
                MentionsUserId = 0, // Needs to be resolved during migration
                CreatedAt = createdAt,
                // Store username for later resolution
                MentionsUser = new User { Username = username }
            });
        }

        return mentions;
    }

    /// <summary>
    /// Extracts post mentions from message content (e.g., #123 or msg#123 patterns).
    /// </summary>
    private static List<PostMentionsPost> ExtractPostMentions(Message message, ResolutionContext context)
    {
        var mentions = new List<PostMentionsPost>();
        
        if (string.IsNullOrEmpty(message.Body))
            return mentions;

        // Pattern for post ID mentions (#123, msg#123, post#123)
        var postMentionPattern = @"(?:msg#|post#|#)(\d+)";
        var matches = Regex.Matches(message.Body, postMentionPattern, RegexOptions.IgnoreCase);

        var createdAt = Mapping.Converter.UnixTimeStampToDateTime(message.PosterTime) ?? DateTime.UtcNow;

        foreach (Match match in matches)
        {
            if (uint.TryParse(match.Groups[1].Value, out uint postId) && postId != message.IdMsg)
            {
                mentions.Add(new PostMentionsPost
                {
                    PostId = message.IdMsg,
                    MentionsPostId = postId,
                    CreatedAt = createdAt
                });
            }
        }

        return mentions;
    }

    /// <summary>
    /// Extracts tag mentions from message content (e.g., #tagname patterns).
    /// </summary>
    private static List<PostMentionsTag> ExtractTagMentions(Message message, ResolutionContext context)
    {
        var mentions = new List<PostMentionsTag>();
        
        if (string.IsNullOrEmpty(message.Body))
            return mentions;

        // Pattern for tag mentions (#tagname, but not #123)
        var tagMentionPattern = @"#([a-zA-Z][a-zA-Z0-9_\-]+)";
        var matches = Regex.Matches(message.Body, tagMentionPattern, RegexOptions.IgnoreCase);

        var createdAt = Mapping.Converter.UnixTimeStampToDateTime(message.PosterTime) ?? DateTime.UtcNow;

        foreach (Match match in matches)
        {
            var tagName = match.Groups[1].Value;
            
            // In a real implementation, you'd look up the tag ID from the tag name
            // For now, we'll create a placeholder that requires post-processing
            mentions.Add(new PostMentionsTag
            {
                PostId = message.IdMsg,
                MentionsTagId = 0, // Needs to be resolved during migration
                CreatedAt = createdAt,
                // Store tag name for later resolution
                MentionsTag = new Tag { Name = tagName }
            });
        }

        return mentions;
    }

    /// <summary>
    /// Extracts group mentions from message content (e.g., @groupname patterns).
    /// </summary>
    private static List<PostMentionsGroup> ExtractGroupMentions(Message message, ResolutionContext context)
    {
        var mentions = new List<PostMentionsGroup>();
        
        if (string.IsNullOrEmpty(message.Body))
            return mentions;

        // Pattern for group mentions (e.g., @moderators, @administrators)
        var groupMentionPattern = @"@(administrators?|moderators?|members?|guests?)";
        var matches = Regex.Matches(message.Body, groupMentionPattern, RegexOptions.IgnoreCase);

        var createdAt = Mapping.Converter.UnixTimeStampToDateTime(message.PosterTime) ?? DateTime.UtcNow;

        foreach (Match match in matches)
        {
            var groupName = match.Groups[1].Value.ToLowerInvariant();
            
            // Map SMF group names to Flarum group IDs
            uint? groupId = groupName switch
            {
                "administrator" or "administrators" => 1, // Admin group
                "moderator" or "moderators" => 4, // Mod group  
                "member" or "members" => 3, // Member group
                "guest" or "guests" => 2, // Guest group
                _ => null
            };

            if (groupId.HasValue)
            {
                mentions.Add(new PostMentionsGroup
                {
                    PostId = message.IdMsg,
                    MentionsGroupId = groupId.Value,
                    CreatedAt = createdAt
                });
            }
        }

        return mentions;
    }
}

/// <summary>
/// Helper class for mention data that needs post-processing to resolve IDs.
/// </summary>
public class MentionResolutionData
{
    public uint PostId { get; set; }
    public string? Username { get; set; }
    public string? TagName { get; set; }
    public DateTime CreatedAt { get; set; }
}