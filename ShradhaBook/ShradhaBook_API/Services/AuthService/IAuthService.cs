namespace ShradhaBook_API.Services.AuthService;

public interface IAuthService
{
    Task<UserLoginResponse?> Login(UserLoginRequest request);
    Task<RefreshTokenResponse?> RefreshToken(RefreshTokenRequest request);
    Task<string?> Logout(int id);
}