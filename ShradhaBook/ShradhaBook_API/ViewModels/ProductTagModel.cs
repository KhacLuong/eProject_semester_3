using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels;

public class ProductTagModel
{
    public int Id { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public int TagId { get; set; }

    [Required] public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedAt { get; set; }
}