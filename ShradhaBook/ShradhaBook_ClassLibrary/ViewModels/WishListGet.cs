namespace ShradhaBook_ClassLibrary.ViewModels;

public class WishListGet
{
    public WishListGet(int id, string? productName, DateTime? createdAt, DateTime? updatedAt)
    {
        Id = id;
        ProductName = productName;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public WishListGet()
    {
    }

    public int Id { get; set; }
    public string? ProductName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}