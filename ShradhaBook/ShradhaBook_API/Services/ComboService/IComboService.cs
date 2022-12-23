using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ComboService
{
    public interface IComboService
    {
        Task<List<ComboModel>> GetAllComboAsync(string? name, int status = 0, int sortBy = 0);
        Task<ComboModel> GetComboAsync(int id);
        Task<int> AddComboAsync(ComboModel model);
        Task DeleteComboAsync(int id);
        Task<int> UpdateComboAsync(int id, ComboModel model);
    }
}
