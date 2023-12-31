﻿namespace ShradhaBook_API.Services.AddressService;

public interface IAddressService
{
    Task<UserInfo?> CreateAddress(AddAddressRequest request);
    Task<List<Address>> GetAllAddresses(int userInfoId);
    Task<Address?> UpdateAddress(int id, Address request);
    Task<Address?> DeleteAddress(int id);
    Task<double?> GetDistance(List<double> destination);
    Task<List<Country>> GetAllCountries();
}