﻿using Microsoft.AspNetCore.Mvc;
using ShradhaBook_API.Models;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public UserController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
        }

        [HttpPost("admin/register")]
        public async Task<ActionResult<User?>> RegisterAdmin(UserRegisterRequest request)
        {
            var user = await _userService.Register(request);
            if (user == null)
            {
                return BadRequest("User already exists.");
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

            return Ok(user);
        }
        
        [HttpPost("customer/register")]
        public async Task<ActionResult<User?>> RegisterCus(UserRegisterRequest request)
        {
            var user = await _userService.Register(request);
            if (user == null)
            {
                return BadRequest("User already exists.");
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

        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPassword(string email)
        {
            var token = await _userService.ForgotPassword(email);
            if (token is null)
                return BadRequest("User not found.");
            // Send email with reset password token
            var emailDto = new EmailDto
            {
                To = email,
                Subject = "Reset password email",
                Body = "<h3> Verify Email </h3><br/>" +
                "<span>" +
                "<a href=\"https://localhost:7000/api/User/verify?token=" + token + "\">Click here</a>" +
                "</span>"
            };
            _emailService.SendEmail(emailDto);
            return Ok("You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<string>> ResetPassword(ResetPasswordRequest request)
        {
            var message = await _userService.ResetPassword(request);
            if (message is null)
                return BadRequest("Invalid Token");

            return Ok("Password successfully reset.");
        }
    }
}
