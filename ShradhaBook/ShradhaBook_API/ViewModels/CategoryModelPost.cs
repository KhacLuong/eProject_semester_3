using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class CategoryModelPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(2)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        public int ParentId { get; set; }


        public string? Status { get; set; }


        public string? Slug { get; set; }

 
        public string? Description { get; set; }

        //public DateTime? CreatedAt { get; set; } = DateTime.Now;

        //public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
