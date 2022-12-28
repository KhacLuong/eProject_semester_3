using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class BlogTagModelGet
    {
        public int Id { get; set; }

        public int? BlogId { get; set; }

        public int? TagId { get; set; }

        public string? CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
    }
}
