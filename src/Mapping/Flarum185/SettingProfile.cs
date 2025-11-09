namespace Mapping.Flarum185;

using AutoMapper;
using Schema.Smf2019;
using Schema.Flarum185;

/// <summary>
/// AutoMapper profile for mapping SMF settings to Flarum settings.
/// Maps SMF global configuration settings to Flarum settings table.
/// Note: Many SMF settings may not have direct equivalents in Flarum.
/// </summary>
public class SettingProfile : Profile
{
    public SettingProfile()
    {
        CreateMap<Schema.Smf2019.Setting, Schema.Flarum185.Setting>()
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => TranslateSettingKey(src.Variable)))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => TranslateSettingValue(src.Variable, src.Value)));
    }

    /// <summary>
    /// Translates SMF setting keys to Flarum equivalents where possible.
    /// </summary>
    private static string TranslateSettingKey(string smfKey)
    {
        return smfKey switch
        {
            "forum_name" => "forum_title",
            "forum_description" => "forum_description", 
            "default_language" => "default_locale",
            "admin_email" => "mail_from",
            "registration_method" => "forum_title", // No direct equivalent
            "posts_per_page" => "discussion_page_size",
            "messages_per_page" => "post_page_size",
            "time_format" => "display_name_format", // Approximation
            "smtp_host" => "mail_host",
            "smtp_port" => "mail_port",
            "smtp_username" => "mail_username",
            "enable_mentions" => "mentions_enabled",
            _ => $"smf_{smfKey}" // Prefix unknown settings
        };
    }

    /// <summary>
    /// Translates SMF setting values to Flarum format where conversion is needed.
    /// </summary>
    private static string? TranslateSettingValue(string smfKey, string smfValue)
    {
        return smfKey switch
        {
            "registration_method" when smfValue == "0" => "open",
            "registration_method" when smfValue == "1" => "approval",
            "registration_method" when smfValue == "2" => "approval",
            "registration_method" when smfValue == "3" => "closed",
            "enable_mentions" when smfValue == "1" => "true",
            "enable_mentions" when smfValue == "0" => "false",
            _ => smfValue // Pass through unchanged
        };
    }
}