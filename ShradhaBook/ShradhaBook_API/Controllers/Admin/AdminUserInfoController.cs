using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[ApiController]
public class AdminUserInfoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserInfoService _userInfoService;

    public AdminUserInfoController(IUserInfoService userInfoService, IMapper mapper)
    {
        _userInfoService = userInfoService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Create user info
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUserInfo(AddUserInfoRequest request)
    {
        var user = await _userInfoService.CreateUserInfo(request);
        if (user == null)
            return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
        return Ok(new ServiceResponse<User> { Data = user, Message = "User info has been created successfully." });
    }

    /// <summary>
    ///     Get user info given user id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsereInfo(int userId)
    {
        var userInfo = await _userInfoService.GetUserInfo(userId);
        if (userInfo == null)
            return NotFound(new ServiceResponse<UserInfo> { Status = false, Message = "User info not found." });
        return Ok(new ServiceResponse<UserInfo> { Data = userInfo });
    }

    /// <summary>
    ///     Update user info given userinfo id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserInfo(int id, UserInfo request)
    {
        var userInfo = await _userInfoService.UpdateUserInfo(id, request);
        if (userInfo == null)
            return NotFound(new ServiceResponse<UserInfo> { Status = false, Message = "User info not found." });
        return Ok(new ServiceResponse<UserInfo>
            { Data = userInfo, Message = "User info has been updated successfully." });
    }

    /// <summary>
    ///     Delete user info given user info id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserInfo(int id)
    {
        var userInfo = await _userInfoService.DeleteUserInfo(id);
        if (userInfo == null)
            return NotFound(new ServiceResponse<UserInfo> { Status = false, Message = "User info not found." });
        return Ok(new ServiceResponse<UserInfo>
            { Data = userInfo, Message = "User info has been deleted successfully." });
    }
}