using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class AdminUserController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public AdminUserController(IUserService userService, IEmailService emailService, IMapper mapper)
    {
        _userService = userService;
        _emailService = emailService;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterRequest request)
    {
        var user = await _userService.Register(request);
        if (user == null)
            return BadRequest(new ServiceResponse<User?> { Status = false, Message = "Email already existed." });

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
        var response = new ServiceResponse<User?>
        {
            Data = user,
            Message = "New user has been created successfully."
        };
        return Ok(response);
    }

    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers(string? query, int page = 1, int itemPerPage = 5)
    {
        var users = await _userService.GetAllUsers(query);

        // Pagination
        var pageCount = Math.Ceiling(users.Count() / (float)itemPerPage);
        users = users.Skip((page - 1) * itemPerPage).Take(itemPerPage).ToList();
        var response = new PaginationResponse<User>
        {
            Data = users,
            ItemPerPage = itemPerPage,
            CurrentPage = page,
            PageSize = (int)pageCount
        };
        return Ok(new ServiceResponse<PaginationResponse<User>> { Data = response });
    }

    [HttpGet("{id}")]
    //[Authorize]
    public async Task<IActionResult> GetSingleUser(int id)
    {
        var user = await _userService.GetSingleUser(id);
        if (user is null)
            return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
        return Ok(new ServiceResponse<User> { Data = user });
    }

    [HttpPut("update/{id}")]
    //[Authorize]
    public async Task<IActionResult> UpdateUser(int id, User request)
    {
        var user = await _userService.UpdateUser(id, request);
        if (user is null)
            return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
        return Ok(new ServiceResponse<User> { Data = user, Message = "User info has been updated successfully." });
    }

    [HttpPut("password/{id}")]
    //[Authorize]
    public async Task<IActionResult> ChangePassword(int id, UserChangePasswordRequest request)
    {
        var user = await _userService.ChangePassword(id, request);
        if (user is null)
            return BadRequest(new ServiceResponse<User> { Status = false, Message = "Old password not match." });
        return Ok(new ServiceResponse<User> { Data = user, Message = "Password has been changed successfully." });
    }

    [HttpDelete("delete/{id}")]
    //[Authorize]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userService.DeleteUser(id);
        if (user is null)
            return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
        return Ok(new ServiceResponse<User> { Data = user, Message = "User has been deleted" });
    }

    [HttpGet("verify")]
    public async Task<IActionResult> VerifyUser(string token)
    {
        var message = await _userService.Verify(token);
        if (message is null)
            return BadRequest(new ServiceResponse<string> { Status = false, Message = "Invalid Token." });
        return Ok(new ServiceResponse<string> { Message = "User has been verified" });
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var token = await _userService.ForgotPassword(email);
        if (token is null)
            return NotFound(new ServiceResponse<string>
                { Status = false, Message = "Email address has not been register." });

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
        return Ok(new ServiceResponse<string> { Message = "Reset password email has been sent." });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        var message = await _userService.ResetPassword(request);
        if (message is null)
            return BadRequest(new ServiceResponse<string>
                { Status = false, Message = "Invalid Token or Token has been expired" });
        return Ok(new ServiceResponse<string> { Message = "Password has been resetted successfully" });
    }
}