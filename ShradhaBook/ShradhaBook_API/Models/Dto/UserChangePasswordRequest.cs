using System.ComponentModel.DataAnnotations;

namespace ShradhaBook_API.Models
{
    public class UserChangePasswordRequest
    {
        [Required]
        public string OldPassword { get; set; } = string.Empty;
        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 character.")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
