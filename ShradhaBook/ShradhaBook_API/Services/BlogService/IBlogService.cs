using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.BlogService
{
    public interface IBlogService
    {

        Task<Object> GetAllBlogAsync(string? tiltle, string? AuthorName, string? status, int pageSize = 20, int pageIndex = 1);
        Task<BlogModelGet> GetBlogAsync(int id);
        Task<BlogModelDetail> GetBlogDetailAsync(int id);
        Task<int> AddBlogAsync(BlogModelPost model);
        Task DeleteBlogAsync(int id);
        Task<int> UpdateBlogAsync(int id, BlogModelPost model);
        Task<bool> IncreseCountViewBlogAsync(int id);
        Task<Object> GetBlogByAuthordIdAsync(int authorId, int pageSize = 20, int pageIndex = 1);
    }
}
