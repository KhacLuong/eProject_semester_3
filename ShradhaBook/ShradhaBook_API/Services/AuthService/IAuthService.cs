namespace ShradhaBook_API.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> Login(UserLoginRequest request, HttpResponse response);
        Task<string> RefreshToken(int id, HttpRequest request, HttpResponse response);
    }
}
