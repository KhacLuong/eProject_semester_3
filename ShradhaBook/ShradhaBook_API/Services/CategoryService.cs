using ShradhaBook_API.Models;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services
{
    public class CategoryService : ICategoryService
    {

        public DataContext context;
        public CategoryService(DataContext context)
        {
            this.context = context;
        }

        public ICollection<Category> getViewCategories(string? name, string? code, int? status = 0, int sortBy = 0)
        {
            var allCategories = context.Categories
               .Where(m => m.Name.ToLower().Contains(String.IsNullOrEmpty(name) ? "" : name.ToLower().Trim())
               && m.Code.ToLower().Contains(String.IsNullOrEmpty(code) ? "" : code.ToLower().Trim()))
               .Where(m => m.Status == status).ToList();

            return allCategories;

        }
    }
}
