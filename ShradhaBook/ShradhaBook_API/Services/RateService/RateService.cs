using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;
using Z.EntityFramework.Plus;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        var query = await (from O in _context.Orders.Where(o => o.UserId == model.UserId & o.OrderTracking.Equals(MyStatusCode.ORDER_DONE_RESUL))
                join OI in _context.OrderItems.Where(oi => oi.ProductId == model.ProductId)
                on O.Id equals OI.OrderId

                select new
                {
                    O.UserId,
                    OI.ProductId,
                }).ToListAsync();

        if (query == null || query.Count == 0)
        {
            return MyStatusCode.NOTFOUD_ORDER;
        }
        var checkCommentExists = _context.Comments.Any(c => c.Id == model.CommentId);
        if (!checkCommentExists)
        {
            model.CommentId = null;
        }
        if(model.Star<1|| model.Star>5) 
        {
            return MyStatusCode.FAILURE;
        }
        var newModel = _mapper.Map<Rate>(model);
        newModel.CreatedAt = DateTime.Now;
        newModel.UpdatedAt = null;
        _context.Rates!.Add(newModel);
        await _context.SaveChangesAsync();
        if (await _context.Products!.FindAsync(newModel.Id) == null) return MyStatusCode.FAILURE;
        return newModel.Id;
    }

    public async  Task DeleteRateAsync(int id)
    {
        var model = _context.Rates!.SingleOrDefault(c => c.Id == id);
        if (model != null)
        {
            _context.Rates!.Remove(model);
            await _context.SaveChangesAsync();
        }
    }

    public Task<List<RateModelGet>> GetRatesByProductIdAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<RateModelGet>> GetRatesByProductIdAsync(int productId, int pageSize = 20, int pageIndex = 1)
    {
        var models = await _context.Rates.Where(c => c.ProductId == productId).ToListAsync();
        var result = PaginatedList<Rate>.Create(models, pageIndex, pageSize);
        var totalPage = PaginatedList<Rate>.totlalPage(models, pageSize);

        return _mapper.Map<List<RateModelGet>>(result);


    }

    public async Task<int> UpdateRateAsync(int id, RateModelPost model)
    {
        if(id== model.Id)
        {
            var modelOld = await _context.Rates!.FindAsync(id);

            if (modelOld==null)
            {
                return MyStatusCode.FAILURE;
            }
            var checkCommentExists = _context.Comments.Any(c => c.Id == model.CommentId);
            if (!checkCommentExists)
            {
                model.CommentId = null;
            }
            if (model.Star < 1 || model.Star > 5)
            {
                return MyStatusCode.FAILURE;
            }
            model.CreatedAt = modelOld.CreatedAt;
            model.UpdatedAt = DateTime.Now;

            var newModel = _mapper.Map<Rate>(model);
            _context.Rates.Update(newModel);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }
        return MyStatusCode.FAILURE;



    }
}