using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.CategotyService
{
    public interface ICategoryService
    {

        Task<List<CategoryModel>> GetAllCategoryAsync(string? name, string? code, int? status = 0, int sortBy = 0);
        Task<CategoryModel> GetCategoryAsync(int id);
        Task<int> AddCategoryAsync(CategoryModel model);
        Task DeleteCategoryAsync(int id);
        Task<int> UpdateCategoryAsync(int id, CategoryModel model);
   
    }
}
