using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Services.TagService
{
    public interface ITagService
    {
        Task<Object> GetAllTagAsync(string? name, int sortBy = 0, int pageSize = 20, int pageIndex = 1);
        Task<TagModelGet> GetTagAsync(int id);
        Task<int> AddTagAsync(TagModelPost model);
        Task DeleteTagAsync(int id);
        Task<int> UpdateTagAsync(int id, TagModelPost model);
        Task<Object> GetTagsByIdProduct(int id, int pageSize = 20, int pageIndex = 1);



    }
}
