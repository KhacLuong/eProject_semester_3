using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShradhaBook_API.Controllers
{
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
        public async Task<ActionResult<UserInfo>> CreateAddress(Address request)
        {
            var userInfo = await _addressService.CreateAddress(request);
            if (userInfo == null)
                return NotFound("User info not found.");
            return Ok(_mapper.Map<UserInfoDto>(userInfo));
        }

        [HttpGet]
        public async Task<ActionResult<Address>> GetAllAddresses(int userInfoId)
        {
            var addresses = await _addressService.GetAllAddresses(userInfoId);
            return Ok(_mapper.Map<AddressDto>(addresses));
        }

        [HttpPut("{userInfoId}")]
        public async Task<ActionResult<Address>> UpdateAddress(int userInfoId, Address request)
        {
            var address = await _addressService.UpdateAddress(userInfoId, request);
            return Ok(_mapper.Map<AddressDto>(address));
        }

        [HttpDelete("{userInfoId}")]
        public async Task<ActionResult<Address>> DeleteAddress(int userInfoId)
        {
            var address = await _addressService.DeleteAddress(userInfoId);
            return Ok(_mapper.Map<AddressDto>(address));
        }
    }
}
