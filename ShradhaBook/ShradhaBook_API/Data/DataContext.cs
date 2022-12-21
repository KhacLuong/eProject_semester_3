using Microsoft.EntityFrameworkCore;

namespace ShradhaBook_API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Navigation(u => u.UserInfo).AutoInclude();
            modelBuilder.Entity<UserInfo>().Navigation(ui => ui.Addresses).AutoInclude();

        }
    }
}
