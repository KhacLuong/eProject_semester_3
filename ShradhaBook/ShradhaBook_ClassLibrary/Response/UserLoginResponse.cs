namespace ShradhaBook_ClassLibrary.Response;

public class UserLoginResponse
{
    public string? Name { get; set; }
    public string Email { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}