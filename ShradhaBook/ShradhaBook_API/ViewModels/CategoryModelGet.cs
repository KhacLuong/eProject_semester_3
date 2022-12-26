using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class CategoryModelGet
    {
        public int Id { get; set; }

        public string Code { get; set; }


        public string Name { get; set; }

        public int ParentId { get; set; }


        public string Status { get; set; }


        public string? Slug { get; set; }

      
        public string? Description { get; set; }

        public string? CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }


    }
}
