using Microsoft.EntityFrameworkCore;
using NLipsum.Core;
using System.Security.Cryptography;

namespace ShradhaBook_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }


        public DbSet<Combo> Combos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ComboProduct> ComboProducts { get; set; }
        public DbSet<ComboTag> ComboTags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

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
            var cat = new List<string> { "Book", "Magazine", "DVD", "CD", "Files", "Stationery", "Utilities" };
            var bookcat = new List<string> { "Arts & Photography","Biographies & Memoirs","Business & Money","Calendars",
            "Children's Books","Christian Books & Bibles","Comics & Graphic Novels","Computers & Technology","Cookbooks, Food & Wine",
            "Crafts, Hobbies & Home","Education & Teaching","Engineering & Transportation","Health, Fitness & Dieting","History",
            "Humor & Entertainment","Law","LGBTQ+ Books","Literature & Fiction","Medical Books","Mystery, Thriller & Suspense",
            "Parenting & Relationships","Politics & Social Sciences","Reference","Religion & Spirituality","Romance","Science & Math",
            "Science Fiction & Fantasy","Self-Help","Sports & Outdoors","Teen & Young Adult","Test Preparation","Travel"};
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
                    }
				);
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
                    }
		);
                var address1 = generator.GenerateWords(6);
                var address2 = generator.GenerateWords(6);
                modelBuilder.Entity<Address>().HasData(
                    new Address
                    {
                        Id = i * 2 - 1,
                        AddressLine1 = address1[0] + " " + address1[1] + " " + address1[2],
                        AddressLine2 = address1[3] + " " + address1[4],
                        District = generator.GenerateWords(1)[0],
                        City = generator.GenerateWords(1)[0],
                        Postcode = _random.Next(10000, 99999),
                        UserInfoId = i,
                        CreateAt = DateTime.Now
                    },
                    new Address
                    {
                        Id = i * 2,
                        AddressLine1 = address2[0] + " " + address2[1] + " " + address2[2],
                        AddressLine2 = address2[3] + " " + address2[4],
                        District = generator.GenerateWords(1)[0],
                        City = generator.GenerateWords(1)[0],
                        Postcode = _random.Next(10000, 99999),
                        UserInfoId = i,
                        CreateAt = DateTime.Now
                    }
		);
            }
        }
    }
}
