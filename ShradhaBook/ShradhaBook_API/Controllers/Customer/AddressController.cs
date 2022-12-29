using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers.Customer;

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

    [HttpPost]
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

    [HttpGet]
    public async Task<IActionResult> GetAllAddresses(int userInfoId)
    {
        var addresses = await _addressService.GetAllAddresses(userInfoId);
        return Ok(new ServiceResponse<IEnumerable<AddressDto>>
        {
            Data = addresses.Select(address => _mapper.Map<AddressDto>(address))
        });
    }

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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
}