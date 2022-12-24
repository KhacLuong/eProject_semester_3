using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.TagService
{
    public interface ITagService
    {
        Task<List<TagModel>> GetAllTagAsync(string? name, int sortBy = 0);
        Task<TagModel> GetTagAsync(int id);
        Task<int> AddTagAsync(TagModel model);
        Task DeleteTagAsync(int id);
        Task<int> UpdateTagAsync(int id, TagModel model);
    }
}
