﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class AdminUserController : ControllerBase
{
    private readonly IEmailService _emailService;
    private readonly IUserService _userService;

    public AdminUserController(IUserService userService, IEmailService emailService)
    {
        _userService = userService;
        _emailService = emailService;
    }

    /// <summary>
    ///     Register new user account
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    ///     Return all user accounts
    /// </summary>
    /// <param name="query"></param>
    /// <param name="page"></param>
    /// <param name="itemPerPage"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllUsers(string? query, int page = 1, int itemPerPage = 5)
    {
        var users = await _userService.GetAllUsers(query);

        // Pagination
        var pageCount = Math.Ceiling(users.Count / (float)itemPerPage);
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

    /// <summary>
    ///     Return user account given by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingleUser(int id)
    {
        var user = await _userService.GetSingleUser(id);
        if (user is null)
            return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
        return Ok(new ServiceResponse<User> { Data = user });
    }

    /// <summary>
    ///     Update user account name
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("update/{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser(int id, User request)
    {
        var user = await _userService.UpdateUser(id, request);
        if (user is null)
            return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
        return Ok(new ServiceResponse<User> { Data = user, Message = "User info has been updated successfully." });
    }

    /// <summary>
    ///     Change user password given id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("password/{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(int id, UserChangePasswordRequest request)
    {
        var user = await _userService.ChangePassword(id, request);
        if (user is null)
            return BadRequest(new ServiceResponse<User> { Status = false, Message = "Old password not match." });
        return Ok(new ServiceResponse<User> { Data = user, Message = "Password has been changed successfully." });
    }

    /// <summary>
    ///     Delete user account given id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("delete/{id:int}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _userService.DeleteUser(id);
        if (user is null)
            return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
        return Ok(new ServiceResponse<User> { Data = user, Message = "User has been deleted" });
    }
}