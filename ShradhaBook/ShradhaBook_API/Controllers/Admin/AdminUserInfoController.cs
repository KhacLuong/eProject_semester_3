using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ShradhaBook_API.Models;

namespace ShradhaBook_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;
        private readonly IMapper _mapper;

        public AdminUserInfoController(IUserInfoService userInfoService, IMapper mapper)
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
            return Ok(new ServiceResponse<User> { Data = user, Message = "User info has been created successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetUsereInfo(int userId)
        {
            var userInfo = await _userInfoService.GetUserInfo(userId);
            if (userInfo == null)
            {
                return NotFound(new ServiceResponse<UserInfo> { Status = false, Message = "User info not found." });
            }
            return Ok(new ServiceResponse<UserInfo> { Data = userInfo });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserInfo(int id, UserInfo request)
        {
            var userInfo = await _userInfoService.UpdateUserInfo(id, request);
            if (userInfo == null)
            {
                return NotFound(new ServiceResponse<UserInfo> { Status = false, Message = "User info not found." });
            }
            return Ok(new ServiceResponse<UserInfo> { Data = userInfo , Message = "User info has been updated successfully."});
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserInfo(int id)
        {
            var userInfo = await _userInfoService.DeleteUserInfo(id);
            if (userInfo == null)
            {
                return NotFound(new ServiceResponse<UserInfo> { Status = false, Message = "User info not found." });
            }
            return Ok(new ServiceResponse<UserInfo> { Data = userInfo, Message = "User info has been deleted successfully." });
        }   
    }
}
