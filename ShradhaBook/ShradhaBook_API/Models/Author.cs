using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]        
        public string Name { get; set; }
     
        public string? Email { get; set; }
    
        public string? Phone { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}
