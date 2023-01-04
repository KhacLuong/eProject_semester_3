using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.Services.WishListService;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.WishListUserService
{
    public class WishListUserService : IWishListUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IWishListService _wishListService;

        public WishListUserService(DataContext context, IMapper mapper, IWishListService WishListService)
        {
            this._context = context;
            this._mapper = mapper;
            this._wishListService = WishListService;

        }

        public async Task<int> AddWishListUserAsync(WishListUserPost model)
        {
            var checkExists = _context.WishListUsers.Any(w => w.WishListId == model.WishListId && w.UserId == model.UserId);
            if (checkExists)
            {
                return MyStatusCode.DUPLICATE;
            }
            if (!(_context.WishLists.Any(w => w.Id == model.WishListId)) || !(_context.Users.Any(u => u.Id == model.UserId)))
            {
                return MyStatusCode.NOTFOUND;
            }
            var newModel = _mapper.Map<WishListUser>(model);
            newModel.CreatedAt = DateTime.Now;
            newModel.UpdatedAt = null;
            _context.WishListUsers!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;
        }



        public async Task<int> AddWishListUserAsync(int userId, int productId)
        {
            WishListPost wishLispost = new WishListPost(0, productId, null, null);
            var addWishList = await _wishListService.AddWishListAsync(wishLispost);
            if (addWishList != MyStatusCode.DUPLICATE && addWishList <= 0)
            {
                return MyStatusCode.FAILURE;
            }
            var wishList = await _wishListService.GetWishListByProductIdAsync(productId);
            var checkExists = _context.WishListUsers.Any(w => w.WishListId == wishList.Id && w.UserId == userId);
            if (checkExists)
            {
                return MyStatusCode.DUPLICATE;
            }
            WishListUserPost  modelPost = new WishListUserPost(0, userId, wishList.Id, null, null);
            var newModel = _mapper.Map<WishListUser>(modelPost);
            newModel.CreatedAt = DateTime.Now;
            newModel.UpdatedAt = null;
            _context.WishListUsers!.Add(newModel);
            await _context.SaveChangesAsync();
            return newModel.Id;
        }


        public async Task<int> DeleteWishListUserAsync(int id)
        {

            var model = _context.WishListUsers!.SingleOrDefault(c => c.Id == id);
            if (model != null)
            {
                _context.WishListUsers!.Remove(model);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;   
        }


        public async Task<int> DeleteWishListUserAsync(int userId, int productId)
        {
            IEnumerable<WishListUser>? query = null;
            query = await (from p in _context.Products.Where(p=>p.Id == productId)
                           from w in p.WishLists
                           from wu in w.WishListUsers
                           where (wu.UserId == userId)
                           select wu).ToListAsync();
            var model  = query.ToList();

            if (model != null)
            {
                _context.WishListUsers!.Remove(model[0]);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }


        public async Task<List<WishListUserGet>> GetAllWishListUserAsync()
        {

            var models = await (from U in _context.Users
                               join WU in _context.WishListUsers
                               on U.Id equals WU.UserId
                               join W in _context.WishLists
                               on WU.WishListId equals W.Id
                               join P in _context.Products
                               on W.ProductId equals P.Id
                               select new WishListUserGet(WU.Id, U.Name,P.Name, WU.CreatedAt, WU.UpdatedAt))!.ToListAsync();

            if (models == null || models.Count == 0)
            {
                return null;
            }
            return _mapper.Map<List<WishListUserGet>>(models);
        }



        public Task<List<WishListUserGet>> GetAllWishListUserAsync(int pageSize = 20, int pageIndex = 1)
        {
            throw new NotImplementedException();
        }

        public async  Task<WishListUserGet> GetWishListUserAsync(int id)
        {
            var models = await(from WU in _context.WishListUsers.Where(w => w.Id == id)
                               join U in _context.Users
                                on WU.UserId equals U.Id
                               join W in _context.WishLists
                               on WU.WishListId equals W.Id
                               join P in _context.Products
                               on W.ProductId equals P.Id
                               select new WishListUserGet(WU.Id, U.Name, P.Name, WU.CreatedAt, WU.UpdatedAt))!.ToListAsync();
            if (models == null || models.Count == 0)
            {
                return null;
            }
            return _mapper.Map<WishListUserGet>(models[0]);
        }



        public async Task<WishListUserGet> GetWishListUsersByUserIdAsync(int id)
        {
            var models = await (from U in _context.Users.Where(u=>u.Id == id)
                               join WU in _context.WishListUsers
                               on U.Id equals WU.UserId
                               join W in _context.WishLists
                               on WU.WishListId equals W.Id
                               join P in _context.Products
                               on W.ProductId equals P.Id
                                select new WishListUserGet(WU.Id, U.Name, P.Name, WU.CreatedAt, WU.UpdatedAt))!.ToListAsync();
            return _mapper.Map<WishListUserGet>(models);
        }



        public async  Task<int> UpdateWishListUserAsync(int id, WishListUserPost model)
        {

            if (id == model.Id)
            {
                if (_context.WishListUsers.Any(c => c.WishListId == model.WishListId && c.UserId != model.UserId && c.Id!=model.Id))
                {
                    return MyStatusCode.DUPLICATE;
                }
                if (!(_context.WishLists.Any(w => w.Id == model.WishListId)) || !(_context.Users.Any(u => u.Id == model.UserId)))
                {
                    return MyStatusCode.NOTFOUND;
                }
                var modelOld = await _context.WishListUsers.FindAsync(id);
                if (modelOld != null)
                {

                    model.CreatedAt = modelOld.CreatedAt;
                    model.UpdatedAt = DateTime.Now;
                }
                else
                {
                    return MyStatusCode.NOTFOUND;
                }


                var updateModel = _mapper.Map<WishListUser>(model);
                _context.WishListUsers.Update(updateModel);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }
    }
}
