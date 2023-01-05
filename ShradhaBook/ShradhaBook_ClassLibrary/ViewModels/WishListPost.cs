namespace ShradhaBook_ClassLibrary.ViewModels;

public class WishListPost
{
    public WishListPost(int id, int? productId, DateTime? createdAt, DateTime? updatedAt)
    {
        Id = id;
        ProductId = productId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public WishListPost()
    {
    }

    public int Id { get; set; }
    public int? ProductId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}