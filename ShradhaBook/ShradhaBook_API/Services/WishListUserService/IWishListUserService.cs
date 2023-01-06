using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.WishListUserService
{
    public interface IWishListUserService
    {
        Task<List<WishListProductGet>> GetAllWishListUserAsync(int pageSize = 20, int pageIndex = 1);
        Task<WishListProductGet> GetWishListUserAsync(int id);
        Task<int> AddWishListUserAsync(int userId, int productId);
        Task<int> AddWishListUserAsync(WishListProductPost model);
        Task<int> DeleteWishListUserAsync(int id);
        Task<int> DeleteWishListUserAsync(int userId, int productId);

        Task<int> UpdateWishListUserAsync(int id, WishListProductPost model);
        Task<List<WishListProductGet>> GetAllWishListUserAsync();
        Task<List<WishListProductGet>> GetWishListUsersByUserIdAsync(int id);

        Task<Object> GetCountWishListAndCart(int userId);
    }
}
