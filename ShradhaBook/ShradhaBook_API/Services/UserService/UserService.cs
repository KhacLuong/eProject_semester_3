using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }

    public async Task<User?> Register(UserRegisterRequest request)
    {
        // check existing user email
        if (_context.Users.Any(u => u.Email == request.Email)) return null;

        // encoding password
        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordSalt = passwordSalt,
            PasswordHash = passwordHash,
            VerificationToken = CreateRandomToken(),
            UserType = request.UserType
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> RegisterCus(UserRegisterRequest request)
    {
        // check existing user email
        if (_context.Users.Any(u => u.Email == request.Email)) return null;

        // encoding password
        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordSalt = passwordSalt,
            PasswordHash = passwordHash,
            VerificationToken = CreateRandomToken()
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<List<User>> GetAllUsers(string? query)
    {
        var users = await _context.Users.Where(u =>
            string.IsNullOrEmpty(query) || string.IsNullOrEmpty(u.Name) || u.Name.ToLower().Contains(query.ToLower())
            || string.IsNullOrEmpty(query) || u.Email.ToLower().Contains(query.ToLower())
        ).ToListAsync();
        return users;
    }

    public async Task<User?> GetSingleUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        return user;
    }

    public async Task<User?> UpdateUser(int id, User request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        user.Name = request.Name;
        user.UpdateAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> ChangePassword(int id, UserChangePasswordRequest request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        if (!VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt)) return null;

        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        return user;
    }

    public async Task<User?> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<string?> Verify(string token)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
        if (user is null)
            return null;

        user.VerifiedAt = DateTime.Now;
        await _context.SaveChangesAsync();

        return "ok";
    }

    public async Task<string?> ForgotPassword(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user is null)
            return null;

        user.PasswordResetToken = CreateRandomToken();
        user.ResetTokenExpires = DateTime.Now.AddHours(1);
        await _context.SaveChangesAsync();

        return user.PasswordResetToken;
    }

    public async Task<string?> ResetPassword(ResetPasswordRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
        if (user is null || user.ResetTokenExpires < DateTime.Now)
            return null;

        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;
        user.PasswordResetToken = null;
        user.ResetTokenExpires = null;

        await _context.SaveChangesAsync();

        return "ok";
    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return computeHash.SequenceEqual(passwordHash);
    }

    private string CreateRandomToken()
    {
        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        if (_context.Users.Any(u => u.VerificationToken == token || u.PasswordResetToken == token))
        {
            token = CreateRandomToken();
        }
        return token;
    }
}