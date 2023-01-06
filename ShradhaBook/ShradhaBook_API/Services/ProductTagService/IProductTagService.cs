namespace ShradhaBook_API.Services.ProductTagService;

public interface IProductTagService
{
    Task<object> GetAllProductTagAsync(string? prodctName, string? TagName, int pageSize = 20, int pageIndex = 1);
    Task<ProductTagGet> GetProductTagAsync(int id);
    Task<int> AddProductTagAsync(ProductTagPost model);
    Task DeleteProductTagAsync(int id);
    Task<int> UpdateProductTagAsync(int id, ProductTagPost model);

    Task<List<ProductTagModel>> GetAllProductTagAsync();
    //Task<List<Object>> GetAllManufacturersAsync();
}