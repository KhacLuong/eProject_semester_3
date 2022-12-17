using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.CategotyService
{
    public interface ICategoryService
    {
        public readonly static int DUPLICATE_CODE = -1;
        public readonly static int DUPLICATE_NAME = -2;
        public readonly static int FAILURE = 0;
        public readonly static int SUCCESS = 1;
        Task<List<CategoryModel>> GetAllCategoryAsync(string? name, string? code, int? status = 0, int sortBy = 0);
        Task<CategoryModel> GetCategoryAsync(int id);
        Task<int> AddCategoryAsync(CategoryModel model);
        Task DeleteCategoryAsync(int id);
        Task<int> UpdateCategoryAsync(int id, CategoryModel model);
   
    }
}
