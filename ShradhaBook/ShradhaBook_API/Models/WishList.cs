using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class WishList
    {
        public int id { get; set; }
        [Required]
        public int ProductId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Product Product { get; set; }

        public virtual ICollection<WishListUser> WishListUsers{ get; set; }

    }
}
