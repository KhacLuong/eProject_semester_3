using AutoMapper;
using ShradhaBook_API.Models.Db;

namespace ShradhaBook_API.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserInfo, UserInfoDto>();
            CreateMap<Address, AddressDto>();
        }
    }
}
