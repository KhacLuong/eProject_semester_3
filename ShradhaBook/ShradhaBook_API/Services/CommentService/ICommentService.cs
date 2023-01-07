namespace ShradhaBook_API.Services.CommentService
{
    public interface ICommentService
    {

        Task<int> AddCommentAsync(CommentModelPost model);
        Task DeleteCommentAsync(int id);
        Task<CommentModelGet> GetCommentById(int id);
        Task<int> UpdateCommentAsync(int id, CommentModelPost model);

    }
}
