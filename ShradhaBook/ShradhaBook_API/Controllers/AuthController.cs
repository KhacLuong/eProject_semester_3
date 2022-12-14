using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Models;
using System.Security.Cryptography;

namespace ShradhaBook_API.Controllers
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
        public async Task<ActionResult<string>> Login(UserLoginRequest request)
        {
            var response = Response;
            var token = await _authService.Login(request, response);
            if (token == null)
            {
                return BadRequest("User and password combination not found.");
            }

            return Ok(token);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(int id)
        {
            var request = Request;
            var response = Response;
            var token = await _authService.RefreshToken(id, request, response);
            if (token == null)
            {
                return BadRequest("User not found.");
            }
            if (token == "1")
            {
                return BadRequest("Invalid token.");
            }
            if (token == "2")
            {
                return BadRequest("Token expired");
            }

            return Ok(token);
        }

        
    }
}
