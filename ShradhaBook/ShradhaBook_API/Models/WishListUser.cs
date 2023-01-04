using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models;

public class WishListUser
{
    public int Id { get; set; }

    [Required] public int UserId { get; set; }

    [Required] public int WishListId { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual WishList WishList { get; set; }
    public virtual User User { get; set; }
}