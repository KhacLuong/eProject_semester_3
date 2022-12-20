using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using ShradhaBook_API.Models.Db;
using ShradhaBook_API.Models;
using ShradhaBook_API.Services.UserInfoService;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ShradhaBook_API.Controllers
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
        public async Task<ActionResult<User>> CreateUserInfo(UserInfo request)
        {
            var user = await _userInfoService.CreateUserInfo(request);
            if (user == null)
                return NotFound("User not found.");
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpGet]
        public async Task<ActionResult<UserInfo>> GetUsereInfo(int userId)
        {
            var userInfo = await _userInfoService.GetUserInfo(userId);
            return Ok(_mapper.Map<UserInfoDto>(userInfo));
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<UserInfo>> UpdateUserInfo(int userId, UserInfo request)
        {
            var userInfo = await _userInfoService.UpdateUserInfo(userId, request);
            return Ok(_mapper.Map<UserInfoDto>(userInfo));
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult<UserInfo>> DeleteUserInfo(int userId)
        {
            var userInfo = await _userInfoService.DeleteUserInfo(userId);
            return Ok(_mapper.Map<UserInfoDto>(userInfo));
        }   
    }
}
