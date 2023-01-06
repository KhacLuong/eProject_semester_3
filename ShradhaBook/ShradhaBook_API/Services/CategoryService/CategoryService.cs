using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Services.CategotyService;

public class CategoryService : ICategoryService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IProductService _productService;


    public CategoryService(DataContext context, IMapper mapper, IProductService productService)
    {
        _context = context;
        _mapper = mapper;
        _productService = productService;
    }

    public async Task<int> AddCategoryAsync(CategoryModelPost model)
    {
        var str = model.Code.Substring(1, 1);
        if (model.Code.Length > 2) return MyStatusCode.FAILURE;
        if (!str.Equals("0") && int.TryParse(str, out var value) == false) return MyStatusCode.FAILURE;
        //if (char.TryParse(model.Code.Substring(0, 1), out var val) == false) return MyStatusCode.FAILURE;
        if (model.Name.Trim() == null) return MyStatusCode.FAILURE;
        if (_context.Categories.Any(c => c.Code == model.Code)) return MyStatusCode.DUPLICATE_CODE;
        if (_context.Categories.Any(c => c.Name == model.Name)) return MyStatusCode.DUPLICATE_NAME;
        model.Slug = Helpers.Helpers.Slugify(model.Name);
        if (!model.Status.Equals(MyStatus.ACTIVE_RESULT.Trim())) model.Status = MyStatus.INACTIVE_RESULT;

        var newModel = _mapper.Map<Category>(model);
        newModel.CreatedAt = DateTime.Now;
        newModel.UpdatedAt = null;
        _context.Categories!.Add(newModel);
        await _context.SaveChangesAsync();
        return newModel.Id;
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var model = _context.Categories!.SingleOrDefault(c => c.Id == id);
        if (model != null)
        {
            _context.Categories!.Remove(model);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<CategoryModelGet>> GetAllCategoryAsync(string? name, string? code, string? status,
        int? sortBy = 0, int pageSize = 20, int pageIndex = 1)
    {
        List<Category>? allModel;
        if (status != null && (status.Equals(MyStatus.ACTIVE_RESULT) || status.Equals(MyStatus.INACTIVE_RESULT)))
            allModel = await _context.Categories
                .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
                            && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
                .Where(m => m.Status == MyStatus.changeStatusCat(status))!.ToListAsync();
        else
            allModel = await _context.Categories
                .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
                            && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))!
                .ToListAsync();


        //if (allModel!=null && status!=null&&(status.Equals(MyStatus.ACTIVE_RESULT)||status.Equals(MyStatus.INACTIVE_RESULT)))
        //{
        //    allModel = (List<Category>)allModel.Where(m => m.Status == MyStatus.changeStatusCat(status));
        //}

        var result = PaginatedList<Category>.Create(allModel, pageIndex, pageSize);
        var totalPage = PaginatedList<Category>.totlalPage(allModel, pageSize);

        return _mapper.Map<List<CategoryModelGet>>(result);
    }

    public async Task<List<CategoryModelGet>> GetAllCategoryAsync()
    {
        var allModel = await _context.Categories.ToListAsync();


        return _mapper.Map<List<CategoryModelGet>>(allModel);
    }

    public async Task<CategoryModelGet> GetCategoryAsync(int id)
    {
        var model = await _context.Categories!.FindAsync(id);
        return _mapper.Map<CategoryModelGet>(model);
    }


    public async Task<int> UpdateCategoryAsync(int id, CategoryModelPost model)
    {
        if (id == model.Id)
        {
            var str = model.Code.Substring(1, 1);
            if (model.Code.Trim().Length > 2) return MyStatusCode.FAILURE;
            if (!str.Equals("0") && int.TryParse(str, out var value) == false) return MyStatusCode.FAILURE;
            if (char.TryParse(model.Code.Substring(0, 1), out var val) == false) return MyStatusCode.FAILURE;
            if (model.Name.Trim() == null) return MyStatusCode.FAILURE;
            if (_context.Categories.Any(c => c.Code == model.Code && c.Id != model.Id))
                return MyStatusCode.DUPLICATE_CODE;
            if (_context.Categories.Any(c => c.Name == model.Name && c.Id != model.Id))
                return MyStatusCode.DUPLICATE_NAME;
            model.Slug = Helpers.Helpers.Slugify(model.Name);
            if (!model.Status.Equals(MyStatus.ACTIVE_RESULT)) model.Status = MyStatus.INACTIVE_RESULT;

            var modelOld = await _context.Categories.FindAsync(id);
            modelOld.Name = model.Name;
            modelOld.Code = model.Code;
            modelOld.Slug = model.Slug;
            modelOld.ParentId = model.ParentId;
            modelOld.Description = model.Description;

            modelOld.Status = MyStatus.changeStatusCat(model.Status);
            modelOld.UpdatedAt = DateTime.Now;


            _context.Categories.Update(modelOld);
            await _context.SaveChangesAsync();
            return MyStatusCode.SUCCESS;
        }

        return MyStatusCode.FAILURE;
    }
    //public async Task<Object> GetListtStatusCategotry()
    //{
    //    List<string> list = MyStatus.getListStatusCategory();

    //    List<Category> listCategory = await _context.Categories.ToListAsync();
    //    List<CategoryModelGet> categoryModelGets = _mapper.Map<List<CategoryModelGet>>(listCategory);
    //    var data = new
    //    {
    //        listStatus = list,
    //        listCategory = categoryModelGets
    //    };

    //    return data;
    //}
}