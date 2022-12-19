using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ComboProductService
{
    public interface IComboProductService
    {
        Task<List<ComboProductModel>> GetAllComboProductAsync();
        Task<ComboProductModel> GetComboProductAsync(int id);
        Task<int> AddComboProductAsync(ComboProductModel model);
        Task DeleteComboProductAsync(int id);
    }
}
