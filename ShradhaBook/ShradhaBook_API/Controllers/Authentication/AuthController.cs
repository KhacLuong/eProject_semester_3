using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Authentication;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///     Login
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var response = Response;
        var userLoginResponse = await _authService.Login(request);
        if (userLoginResponse == null)
            return BadRequest(new ServiceResponse<string>
                { Status = false, Message = "User and password combination not found." });

        return Ok(new ServiceResponse<UserLoginResponse>
            { Data = userLoginResponse, Message = "User logged in successfully." });
    }

    /// <summary>
    ///     Refresh token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
    {
        var refreshTokenResponse = await _authService.RefreshToken(request);
        if (refreshTokenResponse == null)
            return NotFound(new ServiceResponse<string> { Status = false, Message = "User not found." });
        return refreshTokenResponse.RefreshToken switch
        {
            "1" => BadRequest(new ServiceResponse<string> { Status = false, Message = "Invalid token." }),
            "2" => BadRequest(new ServiceResponse<string> { Status = false, Message = "Token expired" }),
            _ => Ok(new ServiceResponse<RefreshTokenResponse>
            {
                Data = refreshTokenResponse, Message = "Refresh token has been re-created."
            })
        };
    }

    /// <summary>
    ///     Logout
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Logout()
    {
        var userId = HttpContext.User.FindFirstValue("Id");
        var message = await _authService.Logout(Convert.ToInt32(userId));
        if (message == null)
            return NotFound(new ServiceResponse<string> { Status = false, Message = "User not found." });
        return Ok(new ServiceResponse<string> { Message = "User logged out." });
    }
}