using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ShradhaBook_API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public AuthService(IConfiguration configuration, DataContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<UserLoginResponse?> Login(UserLoginRequest request, HttpResponse response)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || user.Email != request.Email)
            {
                return null;
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            string token = CreateToken(user);
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user, response);
            var userLoginResponse = new UserLoginResponse { Name = user.Name, Email = user.Email , Token = token };

            return userLoginResponse;
        }

        public async Task<string?> RefreshToken(int id, HttpRequest request, HttpResponse response)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return null;

            var refreshToken = request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return "1";
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return "2";
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken, user, response);

            await _context.SaveChangesAsync();
            return token;
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
                Expires = DateTime.Now.AddDays(7),
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, User user, HttpResponse response)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

            _context.SaveChanges();
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
