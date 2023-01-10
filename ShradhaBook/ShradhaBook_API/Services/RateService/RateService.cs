using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Services.RateService;

public class RateService : IRateService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;


    public RateService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<int> AddRateAsync(RateModelPost model)
    {
        var query = await (from O in _context.Orders.Where(o =>
                (o.UserId == model.UserId) & o.OrderTracking.Equals(MyStatusCode.ORDER_DONE_RESUL))
            join OI in _context.OrderItems.Where(oi => oi.ProductId == model.ProductId)
                on O.Id equals OI.OrderId
            select new
            {
                O.UserId,
                OI.ProductId
            }).ToListAsync();

        if (query == null || query.Count == 0) return MyStatusCode.NOTFOUND_ORDER;

        if (model.Star < 1 || model.Star > 5) return MyStatusCode.FAILURE;
        var newModel = _mapper.Map<Rate>(model);
        newModel.CreatedAt = DateTime.Now;
        newModel.UpdatedAt = null;
        _context.Rates!.Add(newModel);
        await _context.SaveChangesAsync();
        var product = await _context.Products.FindAsync(model.ProductId);
        var countRate = _context.Rates.Where(r => r.ProductId == model.ProductId).Count();
        var oldTotalStar = (countRate - 1) * product.Star;
        var newstar = Math.Round((float)(oldTotalStar + model.Star) / countRate, 2);
        product.Star = (float)newstar;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return newModel.Id;
    }

    public async Task DeleteRateAsync(int id)
    {
        var model = _context.Rates!.SingleOrDefault(c => c.Id == id);
        if (model != null)
        {
            _context.Rates!.Remove(model);
            await _context.SaveChangesAsync();
            var product = await _context.Products.FindAsync(model.ProductId);
            if (product != null)
            {
                var countRate = _context.Rates.Where(r => r.ProductId == model.ProductId).Count();
                var oldTotalStar = (countRate + 1) * product.Star;
                var newstar = Math.Round((float)(oldTotalStar - model.Star) / countRate, 2);
                product.Star = (float)newstar;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task<RateModelGet> GetRateById(int id)
    {
        var model = await _context.Rates!.FindAsync(id);

        return _mapper.Map<RateModelGet>(model);
    }

    public async Task<object> GetRatesAndCommentByProductIdAsync(int productId, int pageSize = 20, int pageIndex = 1)
    {
        IEnumerable<object>? rates = await (from R in _context.Rates.Where(c => c.ProductId == productId)
            join C in _context.Comments
                on R.Id equals C.RateId
            select new
            {
                RateId = R.Id,
                CommentId = C.Id,
                R.ProductId,
                R.UserId,
                R.Star,
                C.Content
            }).ToListAsync();
        var models = rates.ToList();

        var listModel = PaginatedList<object>.Create(models, pageIndex, pageSize);
        var totalPage = PaginatedList<object>.totlalPage(models, pageSize);


        return new
        {
            Rates = models,
            totalRate = models.Count,
            totalPages = totalPage
        };
    }

    public async Task<object> GetRatesByProductIdAsync(int productId, int pageSize = 20, int pageIndex = 1)
    {
        var models = await _context.Rates.Where(c => c.ProductId == productId).ToListAsync();
        var listModel = PaginatedList<Rate>.Create(models, pageIndex, pageSize);
        var totalPage = PaginatedList<Rate>.totlalPage(models, pageSize);


        var result = _mapper.Map<List<RateModelGet>>(listModel);
        return new
        {
            Rates = result,
            totalRate = models.Count,
            totalPages = totalPage
        };
    }

    public async Task<int> UpdateRateAsync(int id, RateModelPost model)
    {
        if (id == model.Id)
        {
            var modelOld = await _context.Rates!.FindAsync(id);
            var checkRateExists = await _context.Rates.AnyAsync(r =>
                r.UserId == model.UserId && r.ProductId == model.ProductId && r.Id != model.Id);
            if (!checkRateExists) return MyStatusCode.FAILURE;
            if (modelOld == null) return MyStatusCode.FAILURE;

            if (model.Star < 1 || model.Star > 5) return MyStatusCode.FAILURE;


            var newModel = _mapper.Map<Rate>(model);
            newModel.CreatedAt = modelOld.CreatedAt;
            newModel.UpdatedAt = DateTime.Now;
            _context.Rates.Update(newModel);
            await _context.SaveChangesAsync();
            var product = await _context.Products.FindAsync(model.ProductId);
            var countRate = _context.Rates.Where(r => r.ProductId == model.ProductId).Count();
            var oldTotalStar = countRate * product.Star;
            var newstar = Math.Round((float)(oldTotalStar + model.Star - modelOld.Star) / countRate, 2);
            product.Star = (float)newstar;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }

        return MyStatusCode.FAILURE;
    }
}