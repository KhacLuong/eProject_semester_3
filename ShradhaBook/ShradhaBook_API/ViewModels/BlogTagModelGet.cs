namespace ShradhaBook_API.ViewModels;

public class BlogTagModelGet
{
    public int Id { get; set; }

        public string? BlogTitle { get; set; }

        public string? TagName { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public BlogTagModelGet(int id, string? blogName, string? tagName, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            BlogTitle = blogName;
            TagName = tagName;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public BlogTagModelGet()
        {
        }
    }
}
