using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class ComboProductModel
    {
        public int Id { get; set; }
        [Required]
        public int ComboId { get; set; }

        [Required]
        public int ProductId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
