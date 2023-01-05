using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels;

public class RateModelPost
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int? CommentId { get; set; }

    [Range(1, 5)] public int Star { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}