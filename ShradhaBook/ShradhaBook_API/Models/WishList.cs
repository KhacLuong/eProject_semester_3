using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class WishList
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User? User { get; set; }

        public virtual ICollection<WishListProduct>? WishListProducts { get; set; }

        public WishList(int id, int userId, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            UserId = userId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
