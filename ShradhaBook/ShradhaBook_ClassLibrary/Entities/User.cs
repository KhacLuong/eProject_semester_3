namespace ShradhaBook_ClassLibrary.Entities;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
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
    public UserInfo? UserInfo { get; set; }
    public List<Order>? Orders { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime? UpdateAt { get; set; }
    public virtual WishList WishList { get; set; }
}