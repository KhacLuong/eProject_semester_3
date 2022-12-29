using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int ParentId { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public string? Slug { get; set; }

        [Required]
        public string? Description { get; set; }

     
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

     
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public virtual ICollection<Product> Products { get; set; }
    }
}
