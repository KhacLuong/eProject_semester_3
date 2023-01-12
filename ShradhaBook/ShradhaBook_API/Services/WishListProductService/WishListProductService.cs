using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Services.WishListProductService;

public class WishListProductService : IWishListProductService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IWishListService _wishListService;

    public WishListProductService(DataContext context, IMapper mapper, IWishListService WishListService)
    {
        _context = context;
        _mapper = mapper;
        _wishListService = WishListService;
    }


    public async Task<int> AddWishListProductAsync(int userId, int productId)
    {
        var wishLispost = new WishListPost(0, userId, DateTime.Now, null);
        var wishList = await _wishListService.GetWishListByUserIdAsync(userId);
        if (wishList == null)
        {
            var addWishList = await _wishListService.AddWishListAsync(wishLispost);
            if (addWishList != MyStatusCode.DUPLICATE && addWishList <= 0) return MyStatusCode.FAILURE;
        }

        var wishList2 = await _wishListService.GetWishListByUserIdAsync(userId);
        var checkExists = _context.WishListProducts.Any(w => w.WishListId == wishList2.Id && w.ProductId == productId);
        if (checkExists) return MyStatusCode.DUPLICATE;
        var modelPost = new WishListProduct(0, productId, wishList2.Id, null, null);
        var newModel = _mapper.Map<WishListProduct>(modelPost);
        newModel.CreatedAt = DateTime.Now;
        newModel.UpdatedAt = null;
        _context.WishListProducts!.Add(newModel);
        await _context.SaveChangesAsync();
        return newModel.Id;
    }

    public Task<int> AddWishListProductAsync(WishListProductPost model)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteWishListProductAsync(int id)
    {
        var model = _context.WishListProducts!.SingleOrDefault(c => c.Id == id);
        if (model != null)
        {
            _context.WishListProducts!.Remove(model);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }

        return MyStatusCode.FAILURE;
    }

    public async Task<int> DeleteWishListProductAsync(int userId, int productId)
    {
        IEnumerable<WishListProduct>? query = null;
        query = await (from wp in _context.WishListProducts.Where(wp => wp.ProductId == productId)
            join w in _context.WishLists.Where(w => w.UserId == userId)
                on wp.WishListId equals w.Id
            select wp).ToListAsync();
        var model = query.ToList();
        if (model != null && model.Count != 0)
        {
            _context.WishListProducts.Remove(model[0]);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }

        return MyStatusCode.FAILURE;
    }

    public async Task<object> GetCountWishListAndCart(int userId)
    {
        var totalWishlistUser = (from w in _context.WishLists.Where(u => u.UserId == userId)
            join wp in _context.WishListProducts
                on w.Id equals wp.WishListId
            select wp).Count();
        var totalOrderIsDone = _context.Orders
            .Where(o => o.UserId == userId && o.OrderTracking.Equals(MyStatusCode.ORDER_DONE_RESUL)).Count();
        var totalOrderIsPreparing = _context.Orders
            .Where(o => o.UserId == userId && !o.OrderTracking.Equals(MyStatusCode.ORDER_DONE_RESUL)).Count();
        return new
        {
            totalWishlist = totalWishlistUser,
            totalOrderIsDone,
            totalOrderIsPreparing
        };
    }

    public async Task<List<WishListProductPost>> GetWishListProductByUserIdAsync(int userId)
    {
        IEnumerable<Product>? query = null;
        query = await (from P in _context.Products
            join WP in _context.WishListProducts
                on P.Id equals WP.ProductId
            join W in _context.WishLists.Where(w => w.UserId == userId)
                on WP.WishListId equals W.Id
            select P)!.ToListAsync();
        return _mapper.Map<List<WishListProductPost>>(query);
    }
}