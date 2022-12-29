using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Services.UserInfoService;

public class UserInfoService : IUserInfoService
{
    private readonly DataContext _context;

    public UserInfoService(DataContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateUserInfo(AddUserInfoRequest request)
    {
        var user = await _context.Users.FindAsync(request.UserId);
        if (user == null)
            return null;

        var userInfo = new UserInfo
        {
            Phone = request.Phone,
            Gender = request.Gender,
            DateofBirth = request.DateofBirth,
            UserId = user.Id
        };

        _context.UserInfo.Add(userInfo);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserInfo?> GetUserInfo(int userId)
    {
        var userInfo = await _context.UserInfo.FirstAsync(i => i.UserId == userId);
        if (userInfo == null)
            return null;
        return userInfo;
    }

    public async Task<UserInfo?> UpdateUserInfo(int id, UserInfo request)
    {
        var userInfo = await _context.UserInfo.FindAsync(id);
        if (userInfo == null)
            return null;

        userInfo.Phone = request.Phone;
        userInfo.Gender = request.Gender;
        userInfo.DateofBirth = request.DateofBirth;
        userInfo.UpdateAt = DateTime.Now;

        await _context.SaveChangesAsync();

        return userInfo;
    }

    public async Task<UserInfo?> DeleteUserInfo(int id)
    {
        var userInfo = await _context.UserInfo.FindAsync(id);
        if (userInfo == null)
            return null;

        _context.UserInfo.Remove(userInfo);
        await _context.SaveChangesAsync();

        return userInfo;
    }
}