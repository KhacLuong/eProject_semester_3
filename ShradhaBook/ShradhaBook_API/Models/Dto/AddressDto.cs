using ShradhaBook_API.Models.Db;
using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models.Dto
{
    public class AddressDto
    {
        [Required]
        public string AddressLine1 { get; set; } = string.Empty;
        public string? AddressLine2 { get; set; }
        [Required]
        public string District { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        public int? Postcode { get; set; }
    }
}
