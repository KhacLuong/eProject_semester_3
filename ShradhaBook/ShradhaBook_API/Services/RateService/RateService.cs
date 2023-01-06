

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Helpers;
using ShradhaBook_API.ViewModels;

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

    public Task<int> AddRateAsync(RateModelPost model)
    {
        throw new NotImplementedException();
    }

    //public Task<int> AddRateAsync(RateModelPost model)
    //{
    //    var checkExistAuthor = _context.Orders.Any(a => a.Id == model.Or);

    //    if (model.Title.Trim().Length == 0) return MyStatusCode.FAILURE;
    //    if (!checkExistAuthor) return MyStatusCode.FAILURE;
    //    model.ViewCount = 0;
    //    model.Slug = Helpers.Helpers.Slugify(model.Title);
    //    var newModel = _mapper.Map<Blog>(model);
    //    newModel.CreatedAt = DateTime.Now;
    //    newModel.UpdatedAt = null;
    //    _context.Blogs!.Add(newModel);
    //    await _context.SaveChangesAsync();
    //    if (await _context.Products!.FindAsync(newModel.Id) == null) return MyStatusCode.FAILURE;
    //    return newModel.Id;
    //}

    public Task DeleteRateAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RateModelGet> GetRateAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<RateModelGet>> GetRatesByProductIdAsync()
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateRateAsync(int id, RateModelPost model)
    {
        throw new NotImplementedException();
    }
}