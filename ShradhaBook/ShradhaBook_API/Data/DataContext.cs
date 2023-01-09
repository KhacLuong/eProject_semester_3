using Microsoft.EntityFrameworkCore;
using NLipsum.Core;
using System.Security.Cryptography;
using System.Text;

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
    public DbSet<Country> Countries { get; set; } = null!;
    
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
        var countries = new List<string>
        {
            "AF", "Afghanistan",
            "AL", "Albania",
            "DZ", "Algeria",
            "AS", "American Samoa",
            "AD", "Andorra",
            "AO", "Angola",
            "AI", "Anguilla",
            "AQ", "Antarctica",
            "AG", "Antigua and Barbuda",
            "AR", "Argentina",
            "AM", "Armenia",
            "AW", "Aruba",
            "AU", "Australia",
            "AT", "Austria",
            "AZ", "Azerbaijan",
            "BS", "Bahamas (the)",
            "BH", "Bahrain",
            "BD", "Bangladesh",
            "BB", "Barbados",
            "BY", "Belarus",
            "BE", "Belgium",
            "BZ", "Belize",
            "BJ", "Benin",
            "BM", "Bermuda",
            "BT", "Bhutan",
            "BO", "Bolivia (Plurinational State of)",
            "BQ", "Bonaire, Sint Eustatius and Saba",
            "BA", "Bosnia and Herzegovina",
            "BW", "Botswana",
            "BV", "Bouvet Island",
            "BR", "Brazil",
            "IO", "British Indian Ocean Territory (the)",
            "BN", "Brunei Darussalam",
            "BG", "Bulgaria",
            "BF", "Burkina Faso",
            "BI", "Burundi",
            "CV", "Cabo Verde",
            "KH", "Cambodia",
            "CM", "Cameroon",
            "CA", "Canada",
            "KY", "Cayman Islands (the)",
            "CF", "Central African Republic (the)",
            "TD", "Chad",
            "CL", "Chile",
            "CN", "China",
            "CX", "Christmas Island",
            "CC", "Cocos (Keeling) Islands (the)",
            "CO", "Colombia",
            "KM", "Comoros (the)",
            "CD", "Congo (the Democratic Republic of the)",
            "CG", "Congo (the)",
            "CK", "Cook Islands (the)",
            "CR", "Costa Rica",
            "HR", "Croatia",
            "CU", "Cuba",
            "CW", "Curaçao",
            "CY", "Cyprus",
            "CZ", "Czechia",
            "CI", "Côte d'Ivoire",
            "DK", "Denmark",
            "DJ", "Djibouti",
            "DM", "Dominica",
            "DO", "Dominican Republic (the)",
            "EC", "Ecuador",
            "EG", "Egypt",
            "SV", "El Salvador",
            "GQ", "Equatorial Guinea",
            "ER", "Eritrea",
            "EE", "Estonia",
            "SZ", "Eswatini",
            "ET", "Ethiopia",
            "FK", "Falkland Islands (the) [Malvinas]",
            "FO", "Faroe Islands (the)",
            "FJ", "Fiji",
            "FI", "Finland",
            "FR", "France",
            "GF", "French Guiana",
            "PF", "French Polynesia",
            "TF", "French Southern Territories (the)",
            "GA", "Gabon",
            "GM", "Gambia (the)",
            "GE", "Georgia",
            "DE", "Germany",
            "GH", "Ghana",
            "GI", "Gibraltar",
            "GR", "Greece",
            "GL", "Greenland",
            "GD", "Grenada",
            "GP", "Guadeloupe",
            "GU", "Guam",
            "GT", "Guatemala",
            "GG", "Guernsey",
            "GN", "Guinea",
            "GW", "Guinea-Bissau",
            "GY", "Guyana",
            "HT", "Haiti",
            "HM", "Heard Island and McDonald Islands",
            "VA", "Holy See (the)",
            "HN", "Honduras",
            "HK", "Hong Kong",
            "HU", "Hungary",
            "IS", "Iceland",
            "IN", "India",
            "ID", "Indonesia",
            "IR", "Iran (Islamic Republic of)",
            "IQ", "Iraq",
            "IE", "Ireland",
            "IM", "Isle of Man",
            "IL", "Israel",
            "IT", "Italy",
            "JM", "Jamaica",
            "JP", "Japan",
            "JE", "Jersey",
            "JO", "Jordan",
            "KZ", "Kazakhstan",
            "KE", "Kenya",
            "KI", "Kiribati",
            "KP", "Korea (the Democratic People's Republic of)",
            "KR", "Korea (the Republic of)",
            "KW", "Kuwait",
            "KG", "Kyrgyzstan",
            "LA", "Lao People's Democratic Republic (the)",
            "LV", "Latvia",
            "LB", "Lebanon",
            "LS", "Lesotho",
            "LR", "Liberia",
            "LY", "Libya",
            "LI", "Liechtenstein",
            "LT", "Lithuania",
            "LU", "Luxembourg",
            "MO", "Macao",
            "MG", "Madagascar",
            "MW", "Malawi",
            "MY", "Malaysia",
            "MV", "Maldives",
            "ML", "Mali",
            "MT", "Malta",
            "MH", "Marshall Islands (the)",
            "MQ", "Martinique",
            "MR", "Mauritania",
            "MU", "Mauritius",
            "YT", "Mayotte",
            "MX", "Mexico",
            "FM", "Micronesia (Federated States of)",
            "MD", "Moldova (the Republic of)",
            "MC", "Monaco",
            "MN", "Mongolia",
            "ME", "Montenegro",
            "MS", "Montserrat",
            "MA", "Morocco",
            "MZ", "Mozambique",
            "MM", "Myanmar",
            "NA", "Namibia",
            "NR", "Nauru",
            "NP", "Nepal",
            "NL", "Netherlands (the)",
            "NC", "New Caledonia",
            "NZ", "New Zealand",
            "NI", "Nicaragua",
            "NE", "Niger (the)",
            "NG", "Nigeria",
            "NU", "Niue",
            "NF", "Norfolk Island",
            "MP", "Northern Mariana Islands (the)",
            "NO", "Norway",
            "OM", "Oman",
            "PK", "Pakistan",
            "PW", "Palau",
            "PS", "Palestine, State of",
            "PA", "Panama",
            "PG", "Papua New Guinea",
            "PY", "Paraguay",
            "PE", "Peru",
            "PH", "Philippines (the)",
            "PN", "Pitcairn",
            "PL", "Poland",
            "PT", "Portugal",
            "PR", "Puerto Rico",
            "QA", "Qatar",
            "MK", "Republic of North Macedonia",
            "RO", "Romania",
            "RU", "Russian Federation (the)",
            "RW", "Rwanda",
            "RE", "Réunion",
            "BL", "Saint Barthélemy",
            "SH", "Saint Helena, Ascension and Tristan da Cunha",
            "KN", "Saint Kitts and Nevis",
            "LC", "Saint Lucia",
            "MF", "Saint Martin (French part)",
            "PM", "Saint Pierre and Miquelon",
            "VC", "Saint Vincent and the Grenadines",
            "WS", "Samoa",
            "SM", "San Marino",
            "ST", "Sao Tome and Principe",
            "SA", "Saudi Arabia",
            "SN", "Senegal",
            "RS", "Serbia",
            "SC", "Seychelles",
            "SL", "Sierra Leone",
            "SG", "Singapore",
            "SX", "Sint Maarten (Dutch part)",
            "SK", "Slovakia",
            "SI", "Slovenia",
            "SB", "Solomon Islands",
            "SO", "Somalia",
            "ZA", "South Africa",
            "GS", "South Georgia and the South Sandwich Islands",
            "SS", "South Sudan",
            "ES", "Spain",
            "LK", "Sri Lanka",
            "SD", "Sudan (the)",
            "SR", "Suriname",
            "SJ", "Svalbard and Jan Mayen",
            "SE", "Sweden",
            "CH", "Switzerland",
            "SY", "Syrian Arab Republic",
            "TW", "Taiwan (Province of China)",
            "TJ", "Tajikistan",
            "TZ", "Tanzania, United Republic of",
            "TH", "Thailand",
            "TL", "Timor-Leste",
            "TG", "Togo",
            "TK", "Tokelau",
            "TO", "Tonga",
            "TT", "Trinidad and Tobago",
            "TN", "Tunisia",
            "TR", "Turkey",
            "TM", "Turkmenistan",
            "TC", "Turks and Caicos Islands (the)",
            "TV", "Tuvalu",
            "UG", "Uganda",
            "UA", "Ukraine",
            "AE", "United Arab Emirates (the)",
            "GB", "United Kingdom of Great Britain and Northern Ireland (the)",
            "UM", "United States Minor Outlying Islands (the)",
            "US", "United States of America (the)",
            "UY", "Uruguay",
            "UZ", "Uzbekistan",
            "VU", "Vanuatu",
            "VE", "Venezuela (Bolivarian Republic of)",
            "VN", "Viet Nam",
            "VG", "Virgin Islands (British)",
            "VI", "Virgin Islands (U.S.)",
            "WF", "Wallis and Futuna",
            "EH", "Western Sahara",
            "YE", "Yemen",
            "ZM", "Zambia",
            "ZW", "Zimbabwe",
            "AX", "Åland Islands"
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
                    Description = _generator.GenerateParagraphs(1)[0],
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
                    Slug = bookcat[i - 8].Replace(" ", "-").ToLower(),
                    ParentId = 1,
                    Status = 1
                }
            );
        }

        for (var i = 0; i < countries.Count; i+=2)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = i/2 + 1,
                    IsoCode = countries[i],
                    Name = countries[i + 1]
                });
        }
    }
}


