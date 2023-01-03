using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Customer;

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

    [HttpPost, Authorize]
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

    [HttpGet, Authorize]
    public async Task<IActionResult> GetUserInfo(int userId)
    {
        var userInfo = await _userInfoService.GetUserInfo(userId);
        return Ok(new ServiceResponse<UserInfoDto> { Data = _mapper.Map<UserInfoDto>(userInfo) });
    }

    [HttpPut("{id:int}"), Authorize]
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