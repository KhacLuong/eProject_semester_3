using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.ComboTagService
{
    public interface IComboTagService
    {

        Task<List<ComboTagModel>> GetAllComboTagAsync();
        Task<ComboTagModel> GetComboTagAsync(int id);
        Task<int> AddComboTagAsync(ComboTagModel model);
        Task DeleteComboTagAsync(int id);
        Task<int> UpdateComboTagAsync(int id, ComboTagModel model);

    }
}
