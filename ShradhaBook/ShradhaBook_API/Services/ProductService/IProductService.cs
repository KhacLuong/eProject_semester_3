using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ProductService
{
    public interface IProductService
    {

        Task<List<ProductModel>> GetAllProductAsync(string? name, string? code, int? categoryId, int? manufactuerId, decimal? price, long quantity, int? status = 0, int? sortBy = 0);
        Task<ProductModel> GetProductAsync(int id);
        Task<int> AddProductAsync(ProductModel model);
        Task DeleteProductAsync(int id);
        Task<int> UpdateProductAsync(int id, ProductModel model);
       
    }
}
