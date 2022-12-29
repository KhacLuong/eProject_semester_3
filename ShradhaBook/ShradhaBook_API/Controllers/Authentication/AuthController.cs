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
            var userLoginResponse = await _authService.Login(request, response);
            if (userLoginResponse == null)
            {
                return BadRequest(new ServiceResponse<string> { Status = false, Message = "User and password combination not found." });
            }

            return Ok(new ServiceResponse<UserLoginResponse> { Data = userLoginResponse, Message = "User logged in successfully." });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var request = Request;
            var response = Response;
            RefreshTokenResponse refreshTokenResponse = await _authService.RefreshToken(refreshToken, request, response);
            if (refreshTokenResponse == null)
            {
                return NotFound(new ServiceResponse<string> { Status = false, Message = "User not found." });
            }
            if (refreshTokenResponse.RefreshToken == "1")
            {
                return BadRequest(new ServiceResponse<string> { Status = false, Message = "Invalid token." });
            }
            if (refreshTokenResponse.RefreshToken == "2")
            {
                return BadRequest(new ServiceResponse<string> { Status = false, Message = "Token expired" });
            }

            return Ok(new ServiceResponse<RefreshTokenResponse> { Data = refreshTokenResponse, Message = "Refresh token has been re-created." });
        }

        [Authorize]
        [HttpPost("logout")]
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
