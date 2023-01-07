using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Admin;

[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
public class AdminAddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    private readonly IMapper _mapper;

    public AdminAddressController(IAddressService addressService, IMapper mapper)
    {
        _addressService = addressService;
        _mapper = mapper;
    }
    /// <summary>
    /// Create new address
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAddress(AddAddressRequest request)
    {
        var userInfo = await _addressService.CreateAddress(request);
        if (userInfo == null)
            return NotFound(new ServiceResponse<UserInfo> { Status = false, Message = "User info not found." });
        return Ok(new ServiceResponse<UserInfo>
            { Data = userInfo, Message = "Address has been created successfully." });
    }
    /// <summary>
    /// Return all address given user info id
    /// </summary>
    /// <param name="userInfoId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAddresses(int userInfoId)
    {
        var addresses = await _addressService.GetAllAddresses(userInfoId);
        return Ok(new ServiceResponse<List<Address>> { Data = addresses });
    }
    /// <summary>
    /// Update address information, given address id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAddress(int id, Address request)
    {
        var address = await _addressService.UpdateAddress(id, request);
        if (address == null)
            return NotFound(new ServiceResponse<Address> { Status = false, Message = "Address not found." });
        return Ok(new ServiceResponse<Address> { Data = address, Message = "Address has been updated successfully." });
    }
    /// <summary>
    /// Delete an address given address id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var address = await _addressService.DeleteAddress(id);
        if (address == null)
            return NotFound(new ServiceResponse<Address> { Status = false, Message = "Address not found." });
        return Ok(new ServiceResponse<Address> { Data = address, Message = "Address has been deleted successfully." });
    }
}