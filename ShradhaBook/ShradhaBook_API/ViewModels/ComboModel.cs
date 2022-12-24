using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class ComboModel
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
    }
}
