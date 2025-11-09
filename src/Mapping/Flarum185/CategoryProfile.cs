using AutoMapper;
using Schema.Flarum185;
using Schema.Smf2019;

namespace Mapping.Flarum185;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, Tag>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => (uint)(src.IdCat + 1000))) // Offset to avoid conflicts with board IDs
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => CreateSlug(src.Name)))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => $"Category: {src.Name}"))
            .ForMember(dest => dest.Color, opt => opt.Ignore())
            .ForMember(dest => dest.BackgroundPath, opt => opt.Ignore())
            .ForMember(dest => dest.BackgroundMode, opt => opt.Ignore())
            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => (int?)src.CatOrder))
            .ForMember(dest => dest.ParentId, opt => opt.Ignore()) // Categories are top-level
            .ForMember(dest => dest.DefaultSort, opt => opt.Ignore())
            .ForMember(dest => dest.IsRestricted, opt => opt.Ignore())
            .ForMember(dest => dest.IsHidden, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionCount, opt => opt.Ignore()) // Will be calculated
            .ForMember(dest => dest.LastPostedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastPostedDiscussionId, opt => opt.Ignore())
            .ForMember(dest => dest.LastPostedUserId, opt => opt.Ignore())
            .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => "fas fa-folder"))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.DiscussionTags, opt => opt.Ignore())
            .ForMember(dest => dest.InverseParent, opt => opt.Ignore())
            .ForMember(dest => dest.LastPostedDiscussion, opt => opt.Ignore())
            .ForMember(dest => dest.LastPostedUser, opt => opt.Ignore())
            .ForMember(dest => dest.Parent, opt => opt.Ignore())
            .ForMember(dest => dest.PostMentionsTags, opt => opt.Ignore())
            .ForMember(dest => dest.TagUsers, opt => opt.Ignore());
    }

    private static string CreateSlug(string name)
    {
        return "category-" + name.ToLowerInvariant()
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
}