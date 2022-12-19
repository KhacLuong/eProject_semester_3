namespace ShradhaBook_API.Services.AddressService
{
    public interface IAddressService
    {
        Task<UserInfo> CreateAddress(AddressDto request);
        Task<List<Address>> GetAllAddresses(int userInfoId);
        Task<Address?> UpdateAddress(int userInfoId, AddressDto request);
        Task<Address?> DeleteAddress(int userInfoId);

    }
}
