using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ProductService;

public interface IProductService
{
    Task<object> GetAllProductAsync(string? name, string? code, string? status, string? categoryName,
        string? authorName, string? manufactuerName,
        decimal? moreThanPrice, decimal? lessThanPrice, long? moreThanQuantity, long? lessThanQuantity, int? sortBy = 0,
        int pageSize = 20, int pageIndex = 1);

    Task<object> GetProductDetailAsync(int id);
    Task<ProductModelGet> GetProductAsync(int id);
    Task<int> AddProductAsync(ProductModelPost model);
    Task DeleteProductAsync(int id);

    Task<int> UpdateProductAsync(int id, ProductModelPost model);
    Task<bool> CheckExistProductByIdCategoryAsync(int categoryId);
    Task<bool> CheckExistProductByIdAuthorAsync(int authorId);
    Task<bool> CheckExistProductByIdManufactuerAsync(int manufacturerId);
    Task<bool> IncreaseViewCountProduct(int id);

    Task<object> GetProductByIdCategoryAsync(int categoryId, int pageSize = 20, int pageIndex = 1);
    Task<object> GetProductByIdAuthorAsync(int authorId, int pageSize = 20, int pageIndex = 1);
    Task<object> GetProductByIdManufactuerAsync(int manufacturerId, int pageSize = 20, int pageIndex = 1);
}