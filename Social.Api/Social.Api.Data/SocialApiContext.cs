using Microsoft.EntityFrameworkCore;
using Social.Api.Data.Model;

namespace Social.Api.Data
{
    public class SocialApiContext : DbContext
    {
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        public SocialApiContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}