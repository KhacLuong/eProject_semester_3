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
                return  ICategoryService.DUPLICATE_CODE;
            }
            if (_context.Categories.Any(c => c.Name == model.Name))
            {
                return ICategoryService.DUPLICATE_NAME;
            }
           
            Category newCategory = _mapper.Map<Category>(model);
            _context.Categories!.Add(newCategory);
            await _context.SaveChangesAsync();
            return newCategory.Id;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = _context.Categories!.SingleOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Categories!.Remove(category);
                await _context.SaveChangesAsync();
            }
         
        }

        public async Task<List<CategoryModel>> GetAllCategoryAsync(string? name, string? code, int? status = 0, int sortBy = 0)
        {
            var allCategories = await _context.Categories
              .Where(m => m.Name.ToLower().Contains(string.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
              && m.Code.ToLower().Contains(string.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
              .Where(m => m.Status == status)!.ToListAsync();
            return _mapper.Map<List<CategoryModel>>(allCategories);
        }

        public async Task<CategoryModel> GetCategoryAsync(int id)
        {
            var category = await _context.Categories!.FindAsync(id);
            return _mapper.Map<CategoryModel>(category);
        }




        public async Task<int> UpdateCategoryAsync(int id, CategoryModel model)
        {
            if (id == model.Id)
            {
                if (_context.Categories.Any(c => c.Code == model.Code&&c.Id!=model.Id))
                {
                    return ICategoryService.DUPLICATE_CODE;
                }
                if (_context.Categories.Any(c => c.Name == model.Name&& c.Id != model.Id))
                {
                    return ICategoryService.DUPLICATE_NAME;
                }
                var updateCategory = _mapper.Map<Category>(model);
                _context.Categories.Update(updateCategory);
                await _context.SaveChangesAsync();
                return ICategoryService.SUCCESS;
            }
            return ICategoryService.FAILURE;
        }
    }
}
