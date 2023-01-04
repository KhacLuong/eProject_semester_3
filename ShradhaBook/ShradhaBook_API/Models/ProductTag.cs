using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models;

public class ProductTag
{
    public int Id { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public int TagId { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; }
    public virtual Tag Tag { get; set; }
}