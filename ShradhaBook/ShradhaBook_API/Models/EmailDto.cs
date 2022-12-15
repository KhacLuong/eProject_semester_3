using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class EmailDto
    {
        [EmailAddress, Required]
        public string To { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
