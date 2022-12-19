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


        public DbSet<Combo> Combos { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ComboProduct> ComboProducts { get; set; }
        public DbSet<ComboTag> ComboTags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }



    }
}
