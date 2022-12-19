using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using ShradhaBook_API.Models.Db;
using ShradhaBook_API.Models;
using ShradhaBook_API.Services.UserInfoService;
using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUserInfo(UserInfoDto request)
        {
            var user = await _userInfoService.CreateUserInfo(request);
            if (user == null)
                return NotFound("User not found.");
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<UserInfo>> GetUsereInfo(int userId)
        {
            var userInfo = await _userInfoService.GetUserInfo(userId);
            return Ok(userInfo);
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<UserInfo>> UpdateUserInfo(int userId, UserInfoDto request)
        {
            var userInfo = await _userInfoService.UpdateUserInfo(userId, request);
            return Ok(userInfo);
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<UserInfo>> DeleteUserInfo(int userId)
        {
            var userInfo = await _userInfoService.DeleteUserInfo(userId);
            return Ok(userInfo);
        }   
    }
}
