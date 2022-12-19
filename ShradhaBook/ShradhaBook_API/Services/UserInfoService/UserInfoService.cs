using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using ShradhaBook_API.Models;
using Wangkanai.Extensions;

namespace ShradhaBook_API.Services.UserInfoService
{
    public class UserInfoService : IUserInfoService
    {
        private readonly DataContext _context;
        public UserInfoService(DataContext context)
        {
            _context = context;
        }
        public async Task<User?> CreateUserInfo(UserInfoDto request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
                return null;

            var userInfo = new UserInfo
            {
                Phone = request.Phone,
                Gender = request.Gender,
                DateofBirth = request.DateofBirth, 
                User = user
            };

            _context.UserInfo.Add(userInfo);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<UserInfo> GetUserInfo(int userId)
        {
           var userInfo = await _context.UserInfo.FirstOrDefaultAsync(i => i.UserId == userId);
            if (userInfo == null)
                return null;
            return userInfo;
        }

        public async Task<UserInfo> UpdateUserInfo(int userId, UserInfoDto request)
        {
            var userInfo = await _context.UserInfo.FirstOrDefaultAsync(i => i.UserId == userId);
            if (userInfo == null)
                return null;

            userInfo.Phone = request.Phone;
            userInfo.Gender = request.Gender;
            userInfo.DateofBirth = request.DateofBirth;
            userInfo.UpdateAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return userInfo;
        }

        public async Task<UserInfo> DeleteUserInfo(int userId)
        {
            var userInfo = await _context.UserInfo.FirstOrDefaultAsync(i => i.UserId == userId);
            if (userInfo == null)
                return null;

            _context.UserInfo.Remove(userInfo);
            await _context.SaveChangesAsync();

            return userInfo;
        }
    }
}
