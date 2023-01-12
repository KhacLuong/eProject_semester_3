namespace ShradhaBook_ClassLibrary.ViewModels;

public class CommentModelPost
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }

    public int ParentId { get; set; }

    public string? Content { get; set; }

    //public DateTime? CreatedAt { get; set; }
    //public DateTime? UpdatedAt { get; set; }

}