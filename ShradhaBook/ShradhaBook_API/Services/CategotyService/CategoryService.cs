using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.CategotyService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryService(DataContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }

        public async Task<int> AddCategoryAsync(CategoryModel model)
        {
            if (_context.Categories.Any(c => c.Code == model.Code))
            {
                return  MyStatusCode.DUPLICATE_CODE;
            }
            if (_context.Categories.Any(c => c.Name == model.Name))
            {
                return MyStatusCode.DUPLICATE_NAME;
            }
           
            Category newModel = _mapper.Map<Category>(model);
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

        public async Task<List<CategoryModel>> GetAllCategoryAsync(string? name, string? code, int? status = 0, int sortBy = 0)
        {
            var allModel = await _context.Categories
              .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
              && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
              .Where(m => m.Status == status)!.ToListAsync();
            return _mapper.Map<List<CategoryModel>>(allModel);
        }

        public async Task<CategoryModel> GetCategoryAsync(int id)
        {
            var model = await _context.Categories!.FindAsync(id);
            return _mapper.Map<CategoryModel>(model);
        }




        public async Task<int> UpdateCategoryAsync(int id, CategoryModel model)
        {
            if (id == model.Id)
            {
                if (_context.Categories.Any(c => c.Code == model.Code&&c.Id!=model.Id))
                {
                    return MyStatusCode.DUPLICATE_CODE;
                }
                if (_context.Categories.Any(c => c.Name == model.Name&& c.Id != model.Id))
                {
                    return MyStatusCode.DUPLICATE_NAME;
                }
                var updateModel = _mapper.Map<Category>(model);
                _context.Categories.Update(updateModel);
                await _context.SaveChangesAsync();
                return MyStatusCode.SUCCESS;
            }
            return MyStatusCode.FAILURE;
        }
    }
}
