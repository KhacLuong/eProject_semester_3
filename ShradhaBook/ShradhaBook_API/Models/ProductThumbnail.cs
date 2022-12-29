namespace ShradhaBook_API.Models;

public class ProductThumbnail
{
    public int Id { get; set; }
    public string ThumbnailName { get; set; }
    public string ThumbnailPath { get; set; }
    public DateTime? CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; }
}