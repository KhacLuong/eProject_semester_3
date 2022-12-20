using System.Reflection.Metadata.Ecma335;

namespace ShradhaBook_API.Models.Dto
{
    public class UserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;
        //public UserInfoDto? UserInfo { get; set; }
    }
}
