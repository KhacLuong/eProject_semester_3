namespace ShradhaBook_API.ViewModels
{
    public class WishListGet
    {
        public int id { get; set; }
        public string? ProductName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public WishListGet(int id, string? productName, DateTime? createdAt, DateTime updatedAt)
        {
            this.id = id;
            ProductName = productName;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public WishListGet()
        {
        }
    }
}
