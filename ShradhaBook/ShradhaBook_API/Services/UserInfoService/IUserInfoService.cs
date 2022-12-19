namespace ShradhaBook_API.Services.UserInfoService
{
    public interface IUserInfoService
    {
        Task<User?> CreateUserInfo(UserInfoDto request);
        Task<UserInfo> GetUserInfo(int userId);
        Task<UserInfo> UpdateUserInfo(int userId, UserInfoDto userInfo);
        Task<UserInfo> DeleteUserInfo(int userId);
    }
}
