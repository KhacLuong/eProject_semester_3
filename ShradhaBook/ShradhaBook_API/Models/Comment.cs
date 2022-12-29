namespace ShradhaBook_API.Models;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }

    public int ParentId { get; set; }
    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual Product Product { get; set; }
    public virtual User User { get; set; }
}