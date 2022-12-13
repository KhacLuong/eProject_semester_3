using Microsoft.EntityFrameworkCore;
using ShradhaBook_API.Models;
using ShradhaBook_API.ViewModels;

namespace ShradhaBook_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<FeatureThumbnail> FeatureThumbnails { get; set; }
        public DbSet<ShradhaBook_API.ViewModels.ViewCategory> ViewCategory { get; set; }
        public DbSet<ShradhaBook_API.ViewModels.ViewProduct> ViewProduct { get; set; }
        public DbSet<ShradhaBook_API.ViewModels.ViewManufacturer> ViewManufacturer { get; set; }
        public DbSet<ShradhaBook_API.ViewModels.ViewFeatureThumbnail> ViewFeatureThumbnail { get; set; }

    }
}
