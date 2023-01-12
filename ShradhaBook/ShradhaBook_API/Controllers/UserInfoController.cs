using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserInfoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserInfoService _userInfoService;

    public UserInfoController(IUserInfoService userInfoService, IMapper mapper)
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
        return Ok(new ServiceResponse<UserDto>
        {
            Data = _mapper.Map<UserDto>(user),
            Message = "User info has been created successfully."
        });
    }

    /// <summary>
    ///     Get user info given user id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserInfo(int userId)
    {
        var userInfo = await _userInfoService.GetUserInfo(userId);
        return Ok(new ServiceResponse<UserInfoDto> { Data = _mapper.Map<UserInfoDto>(userInfo) });
    }

    /// <summary>
    ///     Update user info given user info id
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
            return NotFound(new ServiceResponse<User> { Status = false, Message = "User not found." });
        return Ok(new ServiceResponse<UserInfoDto>
        {
            Data = _mapper.Map<UserInfoDto>(userInfo),
            Message = "User info has been updated successfully."
        });
    }
}