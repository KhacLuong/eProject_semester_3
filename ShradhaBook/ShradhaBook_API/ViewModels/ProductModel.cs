namespace ShradhaBook_API.ViewModels;

public class ProductModel
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public int AuthorId { get; set; }
    public decimal Price { get; set; }
    public long Quantity { get; set; }
    public int ManufacturerId { get; set; }
    public string? Description { get; set; }
    public string? Intro { get; set; }
    public string? ImageProductPath { get; set; }
    public string? ImageProductName { get; set; }
    public string? Status { get; set; }
    public string? Slug { get; set; }
    public float? Star { get; set; }

    public long? ViewCount { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}