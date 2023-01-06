using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using NLipsum.Core;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Data;

public class DataContext : DbContext
{
    // Seeding Data
    private static readonly Random _random = new();
    private static readonly LipsumGenerator generator = new();


    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserInfo> UserInfo { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItems> OrderItems { get; set; } = null!;

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<ProductTag> ProductTags { get; set; } = null!;
    public DbSet<Blog> Blogs { get; set; } = null!;
    public DbSet<BlogTag> BlogTags { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<WishList> WishLists { get; set; } = null!;
    //public DbSet<WishListUser> WishListUsers { get; set; } = null!;
    public DbSet<WishListProduct> WishListProducts { get; set; } = null!;
    public DbSet<Rate> Rates { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;







    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Navigation(u => u.UserInfo).AutoInclude();
        modelBuilder.Entity<UserInfo>().Navigation(ui => ui.Addresses).AutoInclude();
        modelBuilder.Entity<Product>().Navigation(c => c.Manufacturer).AutoInclude();
        modelBuilder.Entity<Order>().Navigation(o => o.OrderItems).AutoInclude();

        var hmac = new HMACSHA512();
        var passwordSalt = hmac.Key;
        var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("abc123"));
        var gender = new List<string> { "female", "male" };
        var cat = new List<string> { "Book", "Magazine", "DVD", "CD", "Files", "Stationery", "Utilities" };
        var bookcat = new List<string>
        {
            "Arts & Photography", "Biographies & Memoirs", "Business & Money", "Calendars",
            "Children's Books", "Christian Books & Bibles", "Comics & Graphic Novels", "Computers & Technology",
            "Cookbooks, Food & Wine",
            "Crafts, Hobbies & Home", "Education & Teaching", "Engineering & Transportation",
            "Health, Fitness & Dieting", "History",
            "Humor & Entertainment", "Law", "LGBTQ+ Books", "Literature & Fiction", "Medical Books",
            "Mystery, Thriller & Suspense",
            "Parenting & Relationships", "Politics & Social Sciences", "Reference", "Religion & Spirituality",
            "Romance", "Science & Math",
            "Science Fiction & Fantasy", "Self-Help", "Sports & Outdoors", "Teen & Young Adult", "Test Preparation",
            "Travel"
        };
        for (var i = 1; i <= 20; i++)
        {
            var name = generator.GenerateWords(1)[0];
            var day = _random.Next(1, 31);
            var month = _random.Next(1, 12);
            if (month == 2 && day > 28)
                day = 28;
            else if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) day = 30;
            var year = 2022 - _random.Next(0, 80);
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
                            + _random.Next(0, 9) + _random.Next(0, 9) + _random.Next(0, 9)
                            + _random.Next(0, 9) + _random.Next(0, 9) + _random.Next(0, 9)
                            + _random.Next(0, 9) + _random.Next(0, 9) + _random.Next(0, 9),
                    Gender = gender[_random.Next(1, 100) < 50 ? 0 : 1],
                    DateofBirth = Convert.ToDateTime(year + "/" + month + "/" + day),
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
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = i,
                    Code = "PR",
                    Name = generator.GenerateWords(1)[0],
                    CategoryId = _random.Next(1, 7),
                    AuthorId = _random.Next(1, 20),
                    Price = _random.Next(100, 10000),
                    Quantity = _random.Next(10, 1000),
                    ManufacturerId = _random.Next(1, 20),
                    Status = 1,
                    ImageProductPath = generator.GenerateWords(1)[0],
                    Slug = generator.GenerateWords(1)[0],
                    ViewCount = _random.Next(0, 100)
                }
            );
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = i,
                    Name = generator.GenerateWords(1)[0]
                }
            );
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer
                {
                    Id = i,
                    Code = "MA",
                    Name = generator.GenerateWords(1)[0]
                }
            );
        }

        for (var i = 1; i <= 7; i++)
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = i,
                    Code = cat[i - 1][..2],
                    Name = cat[i - 1],
                    Description = generator.GenerateParagraphs(1)[0],
                    Slug = cat[i - 1].ToLower(),
                    ParentId = 0,
                    Status = 1
                }
            );
        }
    }


}