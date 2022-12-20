namespace ShradhaBook_API.Services.AddressService
{
    public interface IAddressService
    {
        Task<UserInfo?> CreateAddress(Address request);
        Task<List<Address>> GetAllAddresses(int userInfoId);
        Task<Address?> UpdateAddress(int userInfoId, Address request);
        Task<Address?> DeleteAddress(int userInfoId);
    }
}
