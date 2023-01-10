namespace ShradhaBook_API.Services.WishListProductService;

public interface IWishListProductService
{
    Task<int> AddWishListProductAsync(int userId, int productId);
    Task<int> AddWishListProductAsync(WishListProductPost model);
    Task<int> DeleteWishListProductAsync(int id);
    Task<int> DeleteWishListProductAsync(int userId, int productId);

    Task<List<WishListProductPost>> GetWishListProductByUserIdAsync(int userId);

    Task<object> GetCountWishListAndCart(int userId);
}