namespace ShradhaBook_API.Services.BlogService;

public interface IBlogService
{
    Task<object> GetAllBlogAsync(string? tiltle, string? AuthorName, string? status, int pageSize = 20,
        int pageIndex = 1);

    Task<BlogModelGet> GetBlogAsync(int id);
    Task<BlogModelDetail> GetBlogDetailAsync(int id);
    Task<int> AddBlogAsync(BlogModelPost model);
    Task DeleteBlogAsync(int id);
    Task<int> UpdateBlogAsync(int id, BlogModelPost model);
    Task<bool> IncreseCountViewBlogAsync(int id);
    Task<object> GetBlogByAuthordIdAsync(int authorId, int pageSize = 20, int pageIndex = 1);
    Task<object> GetBlogDetailBySlugAsync(string slug, int pageSize = 20, int pageIndex = 1);
}