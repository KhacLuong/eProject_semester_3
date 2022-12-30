namespace ShradhaBook_API.ViewModels
{
    public class WishListPost
    {
        public int id { get; set; }
        public string? ProductId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public WishListPost(int id, string? productId, DateTime? createdAt, DateTime updatedAt)
        {
            this.id = id;
            ProductId = productId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public WishListPost()
        {
        }
    }
}
