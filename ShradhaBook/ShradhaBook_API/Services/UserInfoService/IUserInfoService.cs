namespace ShradhaBook_API.Services.UserInfoService;

public interface IUserInfoService
{
    Task<User?> CreateUserInfo(AddUserInfoRequest request);
    Task<UserInfo?> GetUserInfo(int userId);
    Task<UserInfo?> UpdateUserInfo(int id, UserInfo userInfo);
    Task<UserInfo?> DeleteUserInfo(int id);
}