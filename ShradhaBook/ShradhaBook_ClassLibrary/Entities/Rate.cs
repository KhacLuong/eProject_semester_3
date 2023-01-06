using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_ClassLibrary.Entities;

public class Rate
{
    public int Id { get; set; }

    [Required] public int UserId { get; set; }

    [Required] public int ProductId { get; set; }

    [Required] public int? CommentId { get; set; }

    [Required] [Range(1, 5)] public int Star { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public virtual Comment? Comment { get; set; }
    public virtual Product Product { get; set; }
    public virtual User User { get; set; }
}