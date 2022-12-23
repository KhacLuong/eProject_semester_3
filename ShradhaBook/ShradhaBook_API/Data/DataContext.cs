using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NLipsum.Core;
using System.Security.Cryptography;

namespace ShradhaBook_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Address> Addresses { get; set; }
        
        static readonly Random _random = new Random();
        static readonly LipsumGenerator generator = new LipsumGenerator();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Navigation(u => u.UserInfo).AutoInclude();
            modelBuilder.Entity<UserInfo>().Navigation(ui => ui.Addresses).AutoInclude();

            var hmac = new HMACSHA512();
            var passwordSalt = hmac.Key;
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("abc123"));
            var gender = new List<string> { "female", "male" };

            for (int i = 1; i <= 20; i++)
            {
                var name = generator.GenerateWords(1)[0];
                var day = _random.Next(1, 31);
                var month = _random.Next(1, 12);
                if (month == 2 && day > 28)
                {
                    day = 28;
                }
                else if ((month == 4 || month == 6 || month == 9 || month == 11) && (day == 31))
                {
                    day = 30;
                }
                var year = (2022 - _random.Next(0, 80));
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = i,
                        Name = name,
                        Email = name + "@mail.com",
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        VerificationToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                        UserType = "user",
                        CreateAt = DateTime.Now
                    });
                modelBuilder.Entity<UserInfo>().HasData(
                    new UserInfo
                    {
                        Id = i,
                        Phone = "0"
                                + _random.Next(0, 9).ToString() + _random.Next(0, 9).ToString() + _random.Next(0, 9).ToString()
                                + _random.Next(0, 9).ToString() + _random.Next(0, 9).ToString() + _random.Next(0, 9).ToString()
                                + _random.Next(0, 9).ToString() + _random.Next(0, 9).ToString() + _random.Next(0, 9).ToString(),
                        Gender = gender[_random.Next(1, 100) < 50 ? 0 : 1],
                        DateofBirth = Convert.ToDateTime(month.ToString() + "/" + day.ToString() + "/" + year.ToString()),
                        UserId = i,
                        CreateAt = DateTime.Now
                    });
                modelBuilder.Entity<Address>().HasData(
                    new Address
                    {
                        Id = i * 2 - 1,
                        AddressLine1 = generator.GenerateWords(3)[0] + generator.GenerateWords(3)[1] + generator.GenerateWords(3)[2],
                        AddressLine2 = generator.GenerateWords(2)[0] + generator.GenerateWords(2)[1],
                        District = generator.GenerateWords(1)[0],
                        City = generator.GenerateWords(1)[0],
                        Postcode = _random.Next(10000, 99999),
                        UserInfoId = i,
                        CreateAt = DateTime.Now
                    },
                    new Address
                    {
                        Id = i * 2,
                        AddressLine1 = generator.GenerateWords(3)[0] + generator.GenerateWords(3)[1] + generator.GenerateWords(3)[2],
                        AddressLine2 = generator.GenerateWords(2)[0] + generator.GenerateWords(2)[1],
                        District = generator.GenerateWords(1)[0],
                        City = generator.GenerateWords(1)[0],
                        Postcode = _random.Next(10000, 99999),
                        UserInfoId = i,
                        CreateAt = DateTime.Now
                    });
            }
        }
    }
}
