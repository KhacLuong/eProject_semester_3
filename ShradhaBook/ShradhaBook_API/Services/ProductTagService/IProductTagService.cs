using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ProductTagService
{
    public interface IProductTagService
    {
        Task<List<ProductTagModel>> GetAllProductTagAsync();
        Task<ProductTagModel> GetProductTagAsync(int id);
        Task<int> AddProductTagAsync(ProductTagModel model);
        Task DeleteProductTagAsync(int id);

    }
}
