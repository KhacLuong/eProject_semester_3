using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    private readonly IMapper _mapper;

    public AddressController(IAddressService addressService, IMapper mapper)
    {
        _addressService = addressService;
        _mapper = mapper;
    }

    /// <summary>
    ///     Create new address
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
        return Ok(new ServiceResponse<UserInfoDto>
        {
            Data = _mapper.Map<UserInfoDto>(userInfo),
            Message = "Address has been created successfully."
        });
    }

    /// <summary>
    ///     Get all addresses given userinfo id
    /// </summary>
    /// <param name="userInfoId"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAddresses(int userInfoId)
    {
        var addresses = await _addressService.GetAllAddresses(userInfoId);
        return Ok(new ServiceResponse<IEnumerable<AddressDto>>
        {
            Data = addresses.Select(address => _mapper.Map<AddressDto>(address))
        });
    }

    /// <summary>
    ///     Update address given address id
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
        return Ok(new ServiceResponse<AddressDto>
        {
            Data = _mapper.Map<AddressDto>(address),
            Message = "Address has been updated successfully."
        });
    }

    /// <summary>
    ///     Delete address given address id
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
        return Ok(new ServiceResponse<AddressDto>
        {
            Data = _mapper.Map<AddressDto>(address),
            Message = "Address has been deleted successfully."
        });
    }

    /// <summary>
    ///     Get distance from store to customer's address, given location
    /// </summary>
    /// <param name="destination"></param>
    /// <returns></returns>
    [HttpPost("distance")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDistance(List<double> destination)
    {
        var travelDistance = await _addressService.GetDistance(destination);
        if (travelDistance == null)
            return BadRequest(new ServiceResponse<double> { Status = false, Message = "API error, please try again." });
        return Ok(new ServiceResponse<double?>
        {
            Data = travelDistance,
            Message = "Successfully get distance."
        });
    }

    /// <summary>
    ///     Get all countries
    /// </summary>
    /// <returns></returns>
    [HttpGet("countries")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCountries()
    {
        var countries = await _addressService.GetAllCountries();
        return Ok(new ServiceResponse<List<Country>> { Data = countries, Message = "Get all countries successfully." });
    }
}