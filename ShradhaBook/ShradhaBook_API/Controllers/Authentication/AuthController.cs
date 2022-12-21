using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ShradhaBook_API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var response = Response;
            var token = await _authService.Login(request, response);
            if (token == null)
            {
                return BadRequest(new ServiceResponse<string> { Status = false, Message = "User and password combination not found." });
            }

            return Ok(new ServiceResponse<string> { Data = token, Message = "User logged in successfully." });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(int id)
        {
            var request = Request;
            var response = Response;
            var token = await _authService.RefreshToken(id, request, response);
            if (token == null)
            {
                return NotFound(new ServiceResponse<string> { Status = false, Message = "User not found." });
            }
            if (token == "1")
            {
                return BadRequest(new ServiceResponse<string> { Status = false, Message = "Invalid token." });
            }
            if (token == "2")
            {
                return BadRequest(new ServiceResponse<string> { Status = false, Message = "Token expired" });
            }

            return Ok(new ServiceResponse<string> { Data = token, Message = "Refresh token has been re-created." });
        }

        [Authorize]
        [HttpDelete("logout")]
        public async Task<ActionResult> Logout()
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var message = await _authService.Logout(Convert.ToInt32(userId));
            if (message == null)
                return NotFound(new ServiceResponse<string> { Status = false, Message = "User not found." });
            return Ok(new ServiceResponse<string> { Message = "User logged out." });
        }
    }
}
