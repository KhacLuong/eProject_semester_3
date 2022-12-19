using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class ComboProduct
    {

        public int Id { get; set; }
        [Required]
        public int ComboId  { get; set; }

        [Required]
        public int ProductId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        public virtual Combo Combo { get; set; }
        public virtual Product Product { get; set; }


    }
}
