using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class BlogTag
    {
        public int Id { get; set; }
        [Required] 
        public int BlogId  { get; set; }
        [Required] 
        public int TagId { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual Tag Tag { get; set; }

    }
}
