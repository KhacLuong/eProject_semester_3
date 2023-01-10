using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UserController(IUserService userService, IEmailService emailService, IMapper mapper)
    {
        _userService = userService;
        _emailService = emailService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Register new customer account
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(UserRegisterRequest request)
    {
        var user = await _userService.RegisterCus(request);
        if (user == null)
            return BadRequest(new ServiceResponse<UserDto> { Status = false, Message = "User already exists." });
        // Send email with verify token
        var emailDto = new EmailDto
        {
            To = user.Email,
            Subject = "Verify email",
            Body = "<h3> Verify Email </h3><br/>" +
                   "<span>" +
                   "<a href=\"https://localhost:7000/api/User/verify?token=" + user.VerificationToken +
                   "\">Click here</a>" +
                   "</span>"
        };
        _emailService.SendEmail(emailDto);
        return Ok(new ServiceResponse<UserDto>
        {
            Data = _mapper.Map<UserDto>(user),
            Message = "Account has been created successfully. Check email to verify."
        });
    }

    /// <summary>
    ///     Get user given user id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingleUser(int id)
    {
        var user = await _userService.GetSingleUser(id);
        if (user is null)
            return NotFound(new ServiceResponse<UserDto> { Status = false, Message = "User not found." });

        return Ok(new ServiceResponse<UserDto> { Data = _mapper.Map<UserDto>(user) });
    }

    /// <summary>
    ///     Update user given user id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(int id, User request)
    {
        var user = await _userService.UpdateUser(id, request);
        if (user is null)
            return NotFound(new ServiceResponse<UserDto> { Status = false, Message = "User not found." });

        return Ok(new ServiceResponse<UserDto>
        {
            Data = _mapper.Map<UserDto>(user),
            Message = "Name has been updated successfully."
        });
    }

    /// <summary>
    ///     Change password given user id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("password/{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword(int id, UserChangePasswordRequest request)
    {
        var user = await _userService.ChangePassword(id, request);
        if (user is null)
            return BadRequest(new ServiceResponse<UserDto> { Status = false, Message = "Old password not correct." });

        return Ok(new ServiceResponse<UserDto>
        {
            Data = _mapper.Map<UserDto>(user),
            Message = "User password has been changed successfully."
        });
    }

    /// <summary>
    ///     Delete user given user id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userService.DeleteUser(id);
        if (user is null)
            return NotFound(new ServiceResponse<UserDto> { Status = false, Message = "User not found." });

        return Ok(new ServiceResponse<UserDto>
        {
            Data = _mapper.Map<UserDto>(user),
            Message = "User has been deleted successfully."
        });
    }

    /// <summary>
    ///     Verify user email given verify token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet("verify")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> VerifyUser(string token)
    {
        var message = await _userService.Verify(token);
        if (message is null)
            return BadRequest(new ServiceResponse<string> { Status = false, Message = "Invalid token." });

        return Ok(new ServiceResponse<string> { Message = "User verified." });
    }

    /// <summary>
    ///     Forgot user password given user email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var token = await _userService.ForgotPassword(email);
        if (token is null)
            return NotFound(new ServiceResponse<string> { Status = false, Message = "User not found." });
        // Send email with reset password token
        var emailDto = new EmailDto
        {
            To = email,
            Subject = "Reset password email",
            Body = "<h3> Reset password </h3><br/>" +
                   "<span>" +
                   "<a href=\"https://localhost:7000/api/User/reset-password?token=" + token + "\">Click here</a>" +
                   "</span>"
        };
        _emailService.SendEmail(emailDto);
        return Ok(new ServiceResponse<string> { Message = "You may now reset your password." });
    }

    /// <summary>
    ///     Reset password
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var message = await _userService.ResetPassword(request);
        if (message is null)
            return BadRequest(new ServiceResponse<string> { Status = false, Message = "Invalid Token" });

        return Ok(new ServiceResponse<string> { Message = "Password successfully reset." });
    }
}