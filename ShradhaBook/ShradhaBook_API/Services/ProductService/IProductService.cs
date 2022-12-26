using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ProductService
{
    public interface IProductService
    {
        Task<Object> GetAllProductAsync(string? name, string? code, string? status,int? categoryId,int? AuthorId, int? manufactuerId, decimal? lowPrice, decimal? hightPrice,  long? lowQuantity, long? hightQuantity,  int? sortBy = 0, int pageSize = 20, int pageIndex = 1);
        Task<Object> GetProductDetailAsync(int id);
        Task<ProductModelGet> GetProductAsync(int id);
        Task<int> AddProductAsync(ProductModelPost model);
        Task DeleteProductAsync(int id);

        Task<int> UpdateProductAsync(int id, ProductModelPost model);
        Task<bool> checkExistProductByIdCategoryAsync(int categoryId);
        Task<bool> checkExistProductByIdAuthorAsync(int authorId);
        Task<bool> checkExistProductByIdManufactuerAsync(int manufacturerId);

        Task<Object> GetProductByIdCategoryAsync(int categoryId, int pageSize = 20, int pageIndex = 1);
        Task<Object> GetProductByIdAuthorAsync(int authorId, int pageSize = 20, int pageIndex = 1);
        Task<Object> GetProductByIdManufactuerAsync(int manufacturerId, int pageSize = 20, int pageIndex = 1);

    }
}
