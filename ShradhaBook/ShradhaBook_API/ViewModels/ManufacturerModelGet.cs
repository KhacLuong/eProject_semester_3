using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.ViewModels
{
    public class ManufacturerModelGet
    {

        public int Id { get; set; }

        public string? Code { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public string? CreatedAt { get; set; } 
        public string? UpdatedAt { get; set; } 

    }
}
