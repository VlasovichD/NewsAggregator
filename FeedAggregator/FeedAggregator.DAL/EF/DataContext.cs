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
    }
}
