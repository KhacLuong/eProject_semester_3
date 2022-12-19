using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class ComboTag
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int ComboId { get; set; }

        [Required]
        public int TagId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Product Product { get; set; }
        public virtual Combo Combo { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
