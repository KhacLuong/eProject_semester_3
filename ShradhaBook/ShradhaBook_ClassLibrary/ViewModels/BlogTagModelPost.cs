namespace ShradhaBook_ClassLibrary.ViewModels;

public class BlogTagModelPost
{
    public int Id { get; set; }

    public int? BlogId { get; set; }

    public int? TagId { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}