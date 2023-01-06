namespace ShradhaBook_API.Services.WishListService;

public interface IWishListService
{
    Task<List<WishListGet>> GetAllWishListAsync(int pageSize = 20, int pageIndex = 1);
    Task<WishListGet> GetWishListAsync(int id);
    Task<int> AddWishListAsync(WishListPost model);
    Task<int> DeleteWishListAsync(int id);
    Task<int> UpdateWishListAsync(int id, WishListPost model);
    Task<List<WishListGet>> GetAllWishListAsync();
    Task<WishListGet> GetWishListByProductIdAsync(int id);
    Task<bool> checkExistsWishListByProductId(int id);
}