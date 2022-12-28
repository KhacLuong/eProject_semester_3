using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class TagModelPost
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public TagModelPost(int id, string name, DateTime? createdAt, DateTime? updatedAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public TagModelPost()
        {
        }
    }
}
