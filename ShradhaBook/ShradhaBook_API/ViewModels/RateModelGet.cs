using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels;

public class RateModelGet
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int? CommentId { get; set; }

    [Range(1, 5)] public int? Star { get; set; }

    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
}