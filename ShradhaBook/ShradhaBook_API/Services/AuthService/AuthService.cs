using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ShradhaBook_API.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;

    public AuthService(IConfiguration configuration, DataContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<UserLoginResponse?> Login(UserLoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null || user.Email != request.Email) return null;

        if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)) return null;

        var token = CreateToken(user);
        var refreshToken = GenerateRefreshToken();
        SetRefreshToken(refreshToken, user);
        var userLoginResponse = new UserLoginResponse
        {
            Name = user.Name,
            Email = user.Email,
            AccessToken = token,
            RefreshToken = refreshToken.Token
        };
        return userLoginResponse;
    }

    public async Task<RefreshTokenResponse?> RefreshToken(RefreshTokenRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user is null)
            return null;

        if (!user.RefreshToken.Equals(request.RefreshToken))
            return new RefreshTokenResponse { AccessToken = "", RefreshToken = "1" };
        if (user.TokenExpires < DateTime.Now) return new RefreshTokenResponse { AccessToken = "", RefreshToken = "2" };

        var token = CreateToken(user);
        var newRefreshToken = GenerateRefreshToken();
        SetRefreshToken(newRefreshToken, user);

        await _context.SaveChangesAsync();
        return new RefreshTokenResponse { AccessToken = token, RefreshToken = newRefreshToken.Token };
    }

    public async Task<string?> Logout(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        user.RefreshToken = "";
        await _context.SaveChangesAsync();
        return "Ok";
    }

    private RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            Expires = DateTime.Now.AddDays(7)
        };

        return refreshToken;
    }

    private void SetRefreshToken(RefreshToken newRefreshToken, User user)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = newRefreshToken.Expires
        };

        user.RefreshToken = newRefreshToken.Token;
        user.TokenCreated = newRefreshToken.Created;
        user.TokenExpires = newRefreshToken.Expires;

        _context.SaveChanges();
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Name, user.Name ?? ""),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.UserType)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(2),
            signingCredentials: creds);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computeHash.SequenceEqual(passwordHash);
    }
}