namespace ShradhaBook_ClassLibrary.ViewModels;

public class ProductThumbnailModel
{
    public int Id { get; set; }
    public string ThumbnailName { get; set; }
    public string ThumbnailPath { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public int ProductId { get; set; }
}