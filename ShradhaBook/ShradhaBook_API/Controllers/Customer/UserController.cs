using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Models;
using System.Data;

namespace ShradhaBook_API.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IEmailService emailService, IMapper mapper)
        {
            _userService = userService;
            _emailService = emailService;
            _mapper = mapper;
        }        
        
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var user = await _userService.RegisterCus(request);
            if (user == null)
            {
                return BadRequest(new ServiceResponse<UserDto> { Status = false, Message = "User already exists." });
            }
            // Send email with verify token
            var emailDto = new EmailDto
            {
                To = user.Email,
                Subject = "Verify email",
                Body = "<h3> Verify Email </h3><br/>" +
                "<span>" +
                "<a href=\"https://localhost:7000/api/User/verify?token=" + user.VerificationToken + "\">Click here</a>" +
                "</span>"
            };
            _emailService.SendEmail(emailDto);
            return Ok(new ServiceResponse<UserDto> 
            { 
                Data = _mapper.Map<UserDto>(user), 
                Message = "Account has been created successfully. Check email to verify." 
            });
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers(string? query)
        {
            var users = await _userService.GetAllUsers(query);
            return Ok(new ServiceResponse<IEnumerable<UserDto>>
            {
                Data = users.Select(user => _mapper.Map<UserDto>(user))
            });
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<IActionResult> GetSingleUser(int id)
        {
            var user = await _userService.GetSingleUser(id);
            if (user is null)
                return NotFound( new ServiceResponse<UserDto> { Status = false, Message = "User not found." });

            return Ok(new ServiceResponse<UserDto> { Data = _mapper.Map<UserDto>(user) });
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateUser(int id, User request)
        {
            var user = await _userService.UpdateUser(id, request);
            if (user is null)
                return NotFound(new ServiceResponse<UserDto> { Status = false, Message = "User not found." });

            return Ok(new ServiceResponse<UserDto> { 
                Data = _mapper.Map<UserDto>(user), 
                Message = "Name and/or Email has been updated successfully."
            });
        }

        [HttpPut("password/{id}")]
        //[Authorize]
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

        [HttpDelete("{id}")]
        //[Authorize]
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

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyUser(string token)
        {
            var message = await _userService.Verify(token);
            if (message is null)
                return BadRequest(new ServiceResponse<string> { Status = false, Message = "Invalid token." });

            return Ok(new ServiceResponse<string> { Message = "User verified." });
        }

        [HttpPost("forgot-password")]
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

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var message = await _userService.ResetPassword(request);
            if (message is null)
                return BadRequest(new ServiceResponse<string> { Status = false, Message = "Invalid Token" });

            return Ok(new ServiceResponse<string> { Message = "Password successfully reset." });
        }
    }
}
