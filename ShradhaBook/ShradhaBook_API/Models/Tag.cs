using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; }
        public virtual ICollection<BlogTag> BlogTags { get; set; }

    }
}
