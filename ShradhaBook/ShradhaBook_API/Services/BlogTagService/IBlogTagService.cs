using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.BlogTagService;

public interface IBlogTagService
{
    Task<object> GetAllBlogTagAsync(string? blogTitle, string? TagName, int pageSize = 20, int pageIndex = 1);
    Task<BlogTagModelGet> GetBlogTagAsync(int id);
    Task<int> AddBlogTagAsync(BlogTagModelPost model);
    Task DeleteBlogTagAsync(int id);
    Task<int> UpdateBlogTagAsync(int id, BlogTagModelPost model);
}