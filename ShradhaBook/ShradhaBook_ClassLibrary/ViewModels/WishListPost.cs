using System.ComponentModel.DataAnnotations;
namespace ShradhaBook_ClassLibrary.ViewModels;

    public class WishListPost
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public WishListPost(int id, int userId, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            UserId = userId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public WishListPost()
        {
        }
    }

