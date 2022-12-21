using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 character.")]
        public string Password { get; set; } = string.Empty;
    }
}
