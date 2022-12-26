namespace ShradhaBook_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
        public int ManufacturerId { get; set; }
        public string? Description { get; set; }
        public string? Intro { get; set; }
        public string? ImageProductPath { get; set; }
        public string? ImageProductName { get; set; }
        public int Status { get; set; }
        public string? Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ProductThumbnail> ProductThumbnails { get; set; }

        public virtual Category Category { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<ComboProduct> ComboProducts { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; }


        public OrderItems OrderItems { get; set; } = null!;

    }

}
