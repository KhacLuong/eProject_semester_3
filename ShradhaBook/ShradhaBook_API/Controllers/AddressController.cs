using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        public async Task<ActionResult<UserInfo>> CreateAddress(AddressDto request)
        {
            var userInfo = await _addressService.CreateAddress(request);
            if (userInfo == null)
                return NotFound("User info not found.");
            return Ok(userInfo);
        }

        [HttpGet]
        public async Task<ActionResult<Address>> GetAllAddresses(int userInfoId)
        {
            var addresses = await _addressService.GetAllAddresses(userInfoId);
            return Ok(addresses);
        }

        [HttpPut("{userInfoId}")]
        public async Task<ActionResult<Address>> UpdateAddress(int userInfoId, AddressDto request)
        {
            var address = await _addressService.UpdateAddress(userInfoId, request);
            return Ok(address);
        }

        [HttpDelete("{userInfoId}")]
        public async Task<ActionResult<Address>> DeleteAddress(int userInfoId)
        {
            var address = await _addressService.DeleteAddress(userInfoId);
            return Ok(address);
        }
    }
}
