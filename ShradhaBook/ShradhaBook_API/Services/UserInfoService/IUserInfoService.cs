namespace ShradhaBook_API.Services.UserInfoService
{
    public interface IUserInfoService
    {
        Task<User?> CreateUserInfo(UserInfo request);
        Task<UserInfo?> GetUserInfo(int userId);
        Task<UserInfo?> UpdateUserInfo(int userId, UserInfo userInfo);
        Task<UserInfo?> DeleteUserInfo(int userId);
    }
}
