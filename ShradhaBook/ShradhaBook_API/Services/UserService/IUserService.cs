﻿using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Models;

namespace ShradhaBook_API.Services.UserService
{
    public interface IUserService
    {
        Task<User?> Register(UserRegisterRequest request);
        Task<string> Verify(string token);
        Task<List<User>> GetAllUsers();
        Task<User?> GetSingleUser(int id);
        Task<User?> UpdateUser(int id, User request);
        Task<User?> ChangePassword(int id, UserChangePasswordRequest request);
        Task<User?> DeleteUser(int id);
    }
}
