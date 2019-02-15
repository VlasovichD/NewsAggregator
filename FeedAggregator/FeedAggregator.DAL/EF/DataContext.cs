using FeedAggregator.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeedAggregator.DAL.EF
{
    public class DataContext : DbContext
    {
        public DbSet<Collection> Collections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Feed> Feeds { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        FirstName = "Admin",
                        LastName = "Admin",
                        Username = "Admin",
                        PasswordSalt = hmac.Key,
                        PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Admin")),
                        Role = "Admin" 
                    });

                base.OnModelCreating(modelBuilder);
            }
        }

    }
}
