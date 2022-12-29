﻿namespace ShradhaBook_API.Services.AuthService;

public interface IAuthService
{
    Task<UserLoginResponse?> Login(UserLoginRequest request, HttpResponse response);
    Task<RefreshTokenResponse?> RefreshToken(string refreshToken, HttpRequest request, HttpResponse response);
    Task<string?> Logout(int id);
}