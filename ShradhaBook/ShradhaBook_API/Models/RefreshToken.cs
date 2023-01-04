namespace ShradhaBook_API.Models;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.Now;

    public DateTime Expires { get; set; }

    //public User User { get; set; }
    public int UserId { get; set; }
}