using ShradhaBook_API.Models.Db;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShradhaBook_API.Models
{
    public class User
    {
		public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        public UserInfo UserInfo { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
    }
}
