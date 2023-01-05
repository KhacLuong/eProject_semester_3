using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShradhaBook_API.Models;

public class Product
{
    public int Id { get; set; }

    [Required] public string Code { get; set; }

    [Required] public string Name { get; set; }

    [Required] public int CategoryId { get; set; }

    public int AuthorId { get; set; }

    [Required]
    [Column(TypeName = "decimal(16,4)")]
    public decimal Price { get; set; }

    [Required] public long Quantity { get; set; }

    [Required] public int ManufacturerId { get; set; }

    public string? Description { get; set; }
    public string? Intro { get; set; }

    [Required] public string? ImageProductPath { get; set; }

    public string? ImageProductName { get; set; }

    [Required] public int Status { get; set; }

    [Required] public string? Slug { get; set; }

    [Required] public long? ViewCount { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; } = DateTime.Now;


    public virtual Category Category { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<ProductTag> ProductTags { get; set; }
        public virtual Author Author { get; set; }
        public virtual ICollection<WishList>? WishLists { get; set; }

        public List<OrderItems>? OrderItems { get; set; }
        

    }

}
