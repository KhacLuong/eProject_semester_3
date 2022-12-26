//using Microsoft.EntityFrameworkCore;
//using Org.BouncyCastle.Asn1.Ocsp;
//using ShradhaBook_API.Models.Request;

//namespace ShradhaBook_API.Services.AddressService
//{
//    public class AddressService : IAddressService
//    {
//        private readonly DataContext _context;
//        public AddressService(DataContext context)
//        {
//            _context = context;
//        }
//        public async Task<UserInfo?> CreateAddress(AddAddressRequest request)
//        {
//            var userInfo = await _context.UserInfo.FindAsync(request.UserInfoId);
//            if (userInfo == null) { return null; }

//            var address = new Address
//            {
//                AddressLine1 = request.AddressLine1,
//                AddressLine2 = request.AddressLine2,
//                District = request.District,
//                City = request.City,
//                UserInfo = userInfo
//            };

//            _context.Add(address);
//            await _context.SaveChangesAsync();
//            return userInfo;
//        }
//        public async Task<List<Address>> GetAllAddresses(int userInfoId)
//        {
//            var addresses = await _context.Addresses.Where(addresses => addresses.UserInfoId == userInfoId).ToListAsync();
//            return addresses;
//        }

//        public async Task<Address?> UpdateAddress(int id, Address request)
//        {
//            var address = await _context.Addresses.FindAsync(id);
//            if (address is null)
//                return null;

//            address.AddressLine1 = request.AddressLine1;
//            address.AddressLine2 = request.AddressLine2;
//            address.District = request.District;
//            address.City = request.City;
//            address.UpdateAt = DateTime.Now;

//            await _context.SaveChangesAsync();

//            return address;
//        }
//        public async Task<Address?> DeleteAddress(int id)
//        {
//            var address = await _context.Addresses.FindAsync(id);

//            if (address is null)
//                return null;

//            _context.Addresses.Remove(address);
//            await _context.SaveChangesAsync();

//            return address;

//        }
//    }
//}
