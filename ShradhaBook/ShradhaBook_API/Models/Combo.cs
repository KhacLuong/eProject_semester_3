using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class Combo
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageComboPath { get; set; }

        [Required]
        public string? ImageComboName { get; set; }

        [Required]
        public int Status { get; set; } 

        [Required]
        public string? Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ComboProduct> ComboProducts { get; set; }
        public virtual ICollection<ComboTag> ComboTags { get; set; }
       


    }
}
