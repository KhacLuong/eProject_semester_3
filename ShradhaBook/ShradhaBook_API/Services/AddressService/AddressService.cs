using BingMapsRESTToolkit;
using Microsoft.EntityFrameworkCore;
using Address = ShradhaBook_API.Models.Entities.Address;

namespace ShradhaBook_API.Services.AddressService;

public class AddressService : IAddressService
{
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;

    public AddressService(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<UserInfo?> CreateAddress(AddAddressRequest request)
    {
        var userInfo = await _context.UserInfo.FindAsync(request.UserInfoId);
        if (userInfo == null) return null;

        var coordinates = await GetLocation(request.AddressLine1, request.AddressLine2, request.District, request.City,
            request.Country);

        var address = new Address
        {
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2,
            District = request.District,
            City = request.City,
            Postcode = request.Postcode,
            Country = request.Country,
            UserInfoId = userInfo.Id,
            Latitude = coordinates![0],
            Longitude = coordinates[1]
        };

        _context.Add(address);
        await _context.SaveChangesAsync();
        return userInfo;
    }

    public async Task<List<Address>> GetAllAddresses(int userInfoId)
    {
        var addresses = await _context.Addresses.Where(addresses => addresses.UserInfoId == userInfoId).ToListAsync();
        return addresses;
    }

    public async Task<Address?> UpdateAddress(int id, Address request)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address is null)
            return null;

        var coordinates = await GetLocation(request.AddressLine1, request.AddressLine2, request.District, request.City,
            request.Country);

        address.AddressLine1 = request.AddressLine1;
        address.AddressLine2 = request.AddressLine2;
        address.District = request.District;
        address.City = request.City;
        address.Postcode = request.Postcode;
        address.Country = request.Country;
        address.UpdateAt = DateTime.Now;
        address.Latitude = coordinates![0];
        address.Longitude = coordinates[1];

        await _context.SaveChangesAsync();

        return address;
    }

    public async Task<Address?> DeleteAddress(int id)
    {
        var address = await _context.Addresses.FindAsync(id);

        if (address is null)
            return null;

        _context.Addresses.Remove(address);
        await _context.SaveChangesAsync();

        return address;
    }

    public async Task<double?> GetDistance(List<double> destination)
    {
        var origins = await _context.Addresses.FindAsync(1);
        if (origins == null)
            return null;
        var request = new DistanceMatrixRequest
        {
            BingMapsKey = _configuration.GetSection("BingMaps:Key").Value,
            Origins = new List<SimpleWaypoint>
            {
                new(origins.Latitude, origins.Longitude)
            },
            Destinations = new List<SimpleWaypoint>
            {
                new(destination[0], destination[1])
            }
        };
        var result = await request.Execute();
        if (result.StatusCode != 200)
            return null;
        var distanceMatrix = result.ResourceSets?.FirstOrDefault()?.Resources.FirstOrDefault() as DistanceMatrix;
        return distanceMatrix!.GetCell(0, 0).TravelDistance;
    }

    private async Task<List<double>?> GetLocation(string addressLine1, string? addressLine2, string district,
        string city,
        string country)
    {
        var request = new GeocodeRequest
        {
            BingMapsKey = _configuration.GetSection("BingMaps:Key").Value,
            Address = new SimpleAddress
            {
                CountryRegion = country,
                AddressLine = $"{addressLine1}, {addressLine2}",
                AdminDistrict = district,
                Locality = city
            }
        };
        var result = await request.Execute();
        if (result.StatusCode != 200) return null;
        var toolkitLocation = result.ResourceSets?.FirstOrDefault()
            ?.Resources?.FirstOrDefault() as Location;
        return toolkitLocation!.Point.Coordinates.ToList();
    }
}