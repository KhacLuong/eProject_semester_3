using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class Blog
    {
        
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string avatar { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public long viewCount { get; set; }
        [Required]

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<BlogTag> BlogTags { get; set; }


    }
}
