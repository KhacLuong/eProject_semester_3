using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.CategotyService;

public interface ICategoryService
{
    Task<List<CategoryModelGet>> GetAllCategoryAsync(string? name, string? code, string? status = "Active",
        int? sortBy = 0, int pageSize = 20, int pageIndex = 1);

    Task<CategoryModelGet> GetCategoryAsync(int id);
    Task<int> AddCategoryAsync(CategoryModelPost model);
    Task DeleteCategoryAsync(int id);
    Task<int> UpdateCategoryAsync(int id, CategoryModelPost model);
    Task<List<CategoryModelGet>> GetAllCategoryAsync();
}