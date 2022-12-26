namespace ShradhaBook_API.ViewModels
{
    public class ProductModelGet
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public string Manufacturer { get; set; }
        public string Author { get; set; }
        public string? Description { get; set; }
        public string? Intro { get; set; }
        public string? ImageProductPath { get; set; }
        public string? ImageProductName { get; set; }
        public string Status { get; set; }
        public string? Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ProductModelGet(int id, string code, string name, string category, decimal price, long quantity, string manufacturer, string author, string? description, string? intro, string? imageProductPath, string? imageProductName, string? status, string? slug, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            Code = code;
            Name = name;
            Category = category;
            Price = price;
            Quantity = quantity;
            Manufacturer = manufacturer;
            Author = author;
            Description = description;
            Intro = intro;
            ImageProductPath = imageProductPath;
            ImageProductName = imageProductName;
            Status = status;
            Slug = slug;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public ProductModelGet()
        {
        }
    }
}
