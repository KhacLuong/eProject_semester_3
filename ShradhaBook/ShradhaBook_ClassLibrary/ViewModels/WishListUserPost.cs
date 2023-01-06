namespace ShradhaBook_ClassLibrary.ViewModels;

public class WishListUserPost
{
    public WishListUserPost(int id, int? userId, int? wishListId, DateTime? createdAt, DateTime? updateedAt)
    {
        Id = id;
        UserId = userId;
        WishListId = wishListId;
        CreatedAt = createdAt;
        UpdatedAt = updateedAt;
    }

    public WishListUserPost()
    {
    }

    public int Id { get; set; }
    public int? UserId { get; set; }
    public int? WishListId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}