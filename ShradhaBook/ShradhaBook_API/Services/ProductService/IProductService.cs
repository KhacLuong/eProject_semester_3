using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ProductService
{
    public interface IProductService
    {
        Task<Object> GetAllProductAsync(string? name, string? code, string? status,int? categoryId,int? AuthorId, int? manufactuerId, decimal? lowPrice, decimal? hightPrice,  long? lowQuantity, long? hightQuantity,  int? sortBy = 0, int pageSize = 20, int pageIndex = 1);
        Task<ProductModelGet> GetProductAsync(int id);
        Task<int> AddProductAsync(ProductModelPost model);
        Task DeleteProductAsync(int id);
        Task<int> UpdateProductAsync(int id, ProductModelPost model);
        Task<List<ProductModelGet>> GetProductByIdCategoryAsync(int categoryId);
        Task<List<ProductModelGet>> GetProductByIdAuthorAsync(int authorId);
        Task<List<ProductModelGet>> GetProductByIdManufactuerAsync(int manufacturerId);
     
    }
}
