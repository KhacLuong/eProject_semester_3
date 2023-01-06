using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_ClassLibrary.Entities
{
    public class WishListProduct
    {
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int WishListId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual WishList WishList { get; set; }
        public virtual Product Product { get; set; }

        public WishListProduct(int id, int productId, int wishListId, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            ProductId = productId;
            WishListId = wishListId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
