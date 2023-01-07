using Microsoft.EntityFrameworkCore;
using NLipsum.Core;
using System.Security.Cryptography;
using System.Text;
using ShradhaBook_ClassLibrary.ViewModels;

namespace ShradhaBook_API.Data;

public class DataContext : DbContext
{
    private readonly DataContext _context;
    private readonly Random _random = new();
    private readonly LipsumGenerator _generator = new();

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
        modelBuilder.Entity<User>().HasData(
            new User()
            {
                Id = 1,
                Name = "admin",
                Email = "admin@mail.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                UserType = "Admin",
                CreateAt = DateTime.Now
            });
        modelBuilder.Entity<UserInfo>().HasData(
            new UserInfo()
            {
                Id = 1,
                UserId = 1,
                Phone = "0"
                        + _random.Next(0, 9) + _random.Next(0, 9) + _random.Next(0, 9)
                        + _random.Next(0, 9) + _random.Next(0, 9) + _random.Next(0, 9)
                        + _random.Next(0, 9) + _random.Next(0, 9) + _random.Next(0, 9),
                Gender = gender[_random.Next(1, 100) < 50 ? 0 : 1],
                DateofBirth = Convert.ToDateTime("2000/01/01"),
                CreateAt = DateTime.Now
            });
        modelBuilder.Entity<Address>().HasData(
            new Address()
            {
                Id = 1,
                UserInfoId = 1,
                AddressLine1 = "8 Ton That Thuyet",
                AddressLine2 = "",
                District = "Cau Giay",
                City = "Hanoi",
                Country = "VN",
                CreateAt = DateTime.Now
            });
        for (var i = 2; i <= 20; i++)
        {
            var name = _generator.GenerateWords(1)[0];
            var day = _random.Next(1, 31);
            var month = _random.Next(1, 12);
            day = month switch
            {
                2 when day > 28 => 28,
                4 or 6 or 9 or 11 when day == 31 => 30,
                _ => day
            };
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
                    UserType = "",
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
            var address1 = _generator.GenerateWords(6);
            var address2 = _generator.GenerateWords(6);

            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = i * 2 - 1,
                    AddressLine1 = address1[0] + " " + address1[1] + " " + address1[2],
                    AddressLine2 = address1[3] + " " + address1[4],
                    District = _generator.GenerateWords(1)[0],
                    City = _generator.GenerateWords(1)[0],
                    UserInfoId = i,
                    CreateAt = DateTime.Now
                },
                new Address
                {
                    Id = i * 2,
                    AddressLine1 = address2[0] + " " + address2[1] + " " + address2[2],
                    AddressLine2 = address2[3] + " " + address2[4],
                    District = _generator.GenerateWords(1)[0],
                    City = _generator.GenerateWords(1)[0],
                    UserInfoId = i,
                    CreateAt = DateTime.Now
                }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = i - 1,
                    Code = "PR",
                    Name = _generator.GenerateWords(1)[0],
                    CategoryId = _random.Next(8, 31),
                    AuthorId = _random.Next(1, 20),
                    Price = _random.Next(100, 1000),
                    Quantity = _random.Next(10, 1000),
                    ManufacturerId = _random.Next(1, 20),
                    Status = 1,
                    ImageProductPath = $"https://erojectaspnet.blob.core.windows.net/products/book{i - 1}.png",
                    ImageProductName = $"book{i - 1}",
                    Slug = _generator.GenerateWords(1)[0],
                    ViewCount = _random.Next(0, 100)
                }
            );
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = i - 1,
                    Name = _generator.GenerateWords(1)[0]
                }
            );
            modelBuilder.Entity<Manufacturer>().HasData(
                new Manufacturer
                {
                    Id = i - 1,
                    Code = "MAF",
                    Name = "MAF " + _generator.GenerateWords(1)[0]
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
                    Description = _generator.GenerateParagraphs(1)[0],
                    Slug = cat[i - 1].ToLower(),
                    ParentId = 0,
                    Status = 1
                }
            );
        for (var i = 8; i <= 39; i++)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = i,
                    Code = bookcat[i - 8][..2],
                    Name = bookcat[i - 8],
                    Description = _generator.GenerateParagraphs(1)[0],
                    Slug = bookcat[i - 8].ToLower(),
                    ParentId = 1,
                    Status = 1
                }
            );
        }
    }
}


