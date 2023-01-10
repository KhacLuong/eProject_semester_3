using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Services.WishListService;

public class WishListService : IWishListService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public WishListService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<int> AddWishListAsync(WishListPost model)
    {
        var checkExists = _context.WishLists.Any(w => w.UserId == model.UserId);
        if (checkExists) return MyStatusCode.DUPLICATE;
        if (!_context.Users.Any(p => p.Id == model.UserId)) return MyStatusCode.NOTFOUND;
        var newModel = _mapper.Map<WishList>(model);
        newModel.CreatedAt = DateTime.Now;
        newModel.UpdatedAt = null;
        _context.WishLists!.Add(newModel);
        await _context.SaveChangesAsync();
        return newModel.Id;
    }


    public async Task<int> DeleteWishListAsync(int id)
    {
        var checkExistsReference = await _context.WishListProducts.AnyAsync(w => w.WishListId == id);
        if (checkExistsReference) return MyStatusCode.EXISTSREFERENCE;
        var model = _context.WishLists!.SingleOrDefault(c => c.Id == id);
        if (model != null)
        {
            _context.WishLists!.Remove(model);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }

        return MyStatusCode.FAILURE;
    }

    public Task<object> GetCountWishListAndCart(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<WishListGet> GetWishListByUserIdAsync(int userId)
    {
        var model = _context.WishLists!.SingleOrDefault(c => c.UserId == userId);
        if (model == null) return null;
        return _mapper.Map<WishListGet>(model);
    }


    //public async  Task<List<WishListGet>> GetAllWishListAsync()
    //{


    //    var models = await (from W in _context.WishLists
    //                       join P in _context.Products
    //                       on W.ProductId equals P.Id
    //                       select new WishListGet(W.Id, P.Name, W.CreatedAt, W.UpdatedAt))!.ToListAsync();

    //    if (models == null || models.Count == 0)
    //    {
    //        return null;
    //    }
    //    return _mapper.Map<List<WishListGet>>(models);

    //}

    //public async  Task<List<WishListGet>> GetAllWishListAsync(int pageSize = 20, int pageIndex = 1)
    //{
    //    List<WishList>? allModel = await _context.WishLists!.ToListAsync();
    //    var result = PaginatedList<WishList>.Create(allModel, pageIndex, pageSize);
    //    var totalPage = PaginatedList<WishList>.totlalPage(allModel, pageSize);

    //    return _mapper.Map<List<WishListGet>>(result);
    //}


    //public  async Task<WishListGet> GetWishListAsync(int id)
    //{
    //    var models = await (from W in _context.WishLists.Where(w=>w.Id == id)
    //                        join P in _context.Products
    //                        on W.ProductId equals P.Id
    //                        select new WishListGet(W.Id, P.Name, W.CreatedAt, W.UpdatedAt))!.ToListAsync();
    //    if (models == null || models.Count == 0)
    //    {
    //        return null;
    //    }
    //    return _mapper.Map<WishListGet>(models[0]);
    //}

    //public async Task<WishListGet> GetWishListByProductIdAsync(int id)
    //{
    //    var models = await (from W in _context.WishLists.Where(w => w.Id == id)
    //                        join P in _context.Products
    //                        on W.ProductId equals P.Id
    //                        select new WishListGet(W.Id, P.Name, W.CreatedAt, W.UpdatedAt))!.ToListAsync();
    //    if (models == null || models.Count == 0)
    //    {
    //        return null;
    //    }
    //    return _mapper.Map<WishListGet>(models[0]);
    //}

    //public Task<WishListGet> GetWishListByUserIdAsync(int id)
    //{
    //    throw new NotImplementedException();
    //}

    //public async Task<int> UpdateWishListAsync(int id, WishListPost model)
    //{

    //    if (id == model.Id)
    //    {
    //        if (_context.WishLists.Any(c => c.ProductId == model.ProductId && c.Id != model.Id && c.Id!=model.Id))
    //        {
    //            return MyStatusCode.DUPLICATE;
    //        }
    //        if (!(_context.Products.Any(p => p.Id == model.ProductId)))
    //        {
    //            return MyStatusCode.NOTFOUND;
    //        }
    //        var modelOld = await _context.WishLists.FindAsync(id);
    //        if (modelOld != null)
    //        {

    //            model.CreatedAt = modelOld.CreatedAt;
    //            model.UpdatedAt = DateTime.Now;
    //        }
    //        else
    //        {
    //            return MyStatusCode.NOTFOUND;
    //        }


    //        var updateModel = _mapper.Map<WishList>(model);
    //        _context.WishLists.Update(updateModel);
    //        await _context.SaveChangesAsync();
    //        return MyStatusCode.SUCCESS;
    //    }
    //    return MyStatusCode.FAILURE;
    //}
}