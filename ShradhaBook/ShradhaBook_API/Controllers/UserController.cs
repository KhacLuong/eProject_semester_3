using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Models;
using System.Security.Cryptography;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User?>> Register(UserRegisterRequest request)
        {
            var user = await _userService.Register(request);
            if (user == null)
            {
                return BadRequest("User already exists.");
            }
            
            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetSingleUser(int id)
        {
            var user = await _userService.GetSingleUser(id);
            if (user is null)
                return BadRequest("User not found.");

            return Ok(user);
        }

        [HttpPut("user/{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User request)
        {
            var user = await _userService.UpdateUser(id, request);
            if (user is null)
                return BadRequest("User not found.");

            return Ok(user);
        }

        [HttpPut("password/{id}")]
        public async Task<ActionResult<User>> ChangePassword(int id, UserChangePasswordRequest request)
        {
            var user = await _userService.ChangePassword(id, request);
            if (user is null)
                return BadRequest("User not found.");

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _userService.DeleteUser(id);
            if (user is null)
                return BadRequest("User not found.");
            
            return Ok(user);
        }

        [HttpPost("verify")]
        public async Task<ActionResult<string>> VerifyUser(string token)
        {
            var message = await _userService.Verify(token);
            if (message is null)
                return BadRequest("Invalid token.");

            return Ok("User verified.");
        }
    }
}
