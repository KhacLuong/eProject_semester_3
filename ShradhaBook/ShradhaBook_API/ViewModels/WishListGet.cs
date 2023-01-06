using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class WishListGet
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public WishListGet(int id, int userId, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            UserId = userId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public WishListGet()
        {
        }
    }
}
