namespace ShradhaBook_API.ViewModels
{
    public class WishListGet
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public WishListGet(int id, string? productName, DateTime? createdAt, DateTime? updatedAt)
        {
            this.Id = id;
            ProductName = productName;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public WishListGet()
        {
        }
    }
}
