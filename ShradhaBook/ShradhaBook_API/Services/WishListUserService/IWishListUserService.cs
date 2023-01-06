using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.WishListUserService
{
    public interface IWishListUserService
    {
        Task<List<WishListUserGet>> GetAllWishListUserAsync(int pageSize = 20, int pageIndex = 1);
        Task<WishListUserGet> GetWishListUserAsync(int id);
        Task<int> AddWishListUserAsync(int userId, int productId);
        Task<int> AddWishListUserAsync(WishListUserPost model);
        Task<int> DeleteWishListUserAsync(int id);
        Task<int> DeleteWishListUserAsync(int userId, int productId);

        Task<int> UpdateWishListUserAsync(int id, WishListUserPost model);
        Task<List<WishListUserGet>> GetAllWishListUserAsync();
        Task<List<WishListUserGet>> GetWishListUsersByUserIdAsync(int id);

        Task<Object> GetCountWishListAndCart(int userId);
    }
}
