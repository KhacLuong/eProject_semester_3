using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(3)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Address { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
