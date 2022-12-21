using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace ShradhaBook_API.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IMapper _mapper;

        public UserInfoController(IUserInfoService userInfoService, IMapper mapper)
        {
            _userInfoService = userInfoService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserInfo(AddUserInfoRequest request)
        {
            var user = await _userInfoService.CreateUserInfo(request);
            if (user == null)
                return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
            return Ok(new ServiceResponse<UserDto> 
            { 
                Data = _mapper.Map<UserDto>(user), 
                Message = "User info has been created successfully." 
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUsereInfo(int userId)
        {
            var userInfo = await _userInfoService.GetUserInfo(userId);
            return Ok(new ServiceResponse<UserInfoDto> { Data = _mapper.Map<UserInfoDto>(userInfo) });
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserInfo(int id, UserInfo request)
        {
            var userInfo = await _userInfoService.UpdateUserInfo(id, request);
            if (userInfo == null)
            {
                return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
            }
            return Ok(new ServiceResponse<UserInfoDto>
            {
                Data = _mapper.Map<UserInfoDto>(userInfo),
                Message = "User info has been updated successfully."
            });
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserInfo(int id)
        {
            var userInfo = await _userInfoService.DeleteUserInfo(id);
            if (userInfo == null)
            {
                return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
            }
            return Ok(new ServiceResponse<UserInfoDto>
            {
                Data = _mapper.Map<UserInfoDto>(userInfo),
                Message = "User info has been deleted successfully."
            });
        }   
    }
}
