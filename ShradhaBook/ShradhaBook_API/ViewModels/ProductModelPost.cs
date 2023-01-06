namespace ShradhaBook_API.ViewModels;

public class ProductModelPost
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

    public ProductModelPost(int id, string code, string name, int categoryId, int authorId, decimal price, long quantity, int manufacturerId, string? description, string? intro,
        string? imageProductPath, string? imageProductName, string? status, string? slug, float? star, long? viewCount, DateTime? createdAt, DateTime? updatedAt)
    {
        Id = id;
        Code = code;
        Name = name;
        CategoryId = categoryId;
        AuthorId = authorId;
        Price = price;
        Quantity = quantity;
        ManufacturerId = manufacturerId;
        Description = description;
        Intro = intro;
        ImageProductPath = imageProductPath;
        ImageProductName = imageProductName;
        Status = status;
        Slug = slug;
        Star = star;
        ViewCount = viewCount;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public ProductModelPost()
    {
    }
}