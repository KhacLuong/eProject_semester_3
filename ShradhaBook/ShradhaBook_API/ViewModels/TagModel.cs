using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class TagModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
