namespace ShradhaBook_ClassLibrary.ViewModels;

public class BlogModelGet
{
    public BlogModelGet(int id, string? title, string? description, string? content, string? avatar,
        string? authorName, string? status, string? slug, long? viewCount, DateTime? createdAt, DateTime? updatedAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Content = content;
        this.avatar = avatar;
        AuthorName = authorName;
        Status = status;
        Slug = slug;
        ViewCount = viewCount;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public BlogModelGet()
    {
    }


    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Content { get; set; }

    public string? avatar { get; set; }

    public string? AuthorName { get; set; }

    public string? Status { get; set; }

    public string? Slug { get; set; }

    public long? ViewCount { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}