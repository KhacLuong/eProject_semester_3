using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class TagModelPost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
