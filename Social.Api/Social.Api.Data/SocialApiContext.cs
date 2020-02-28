using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Social.Api.Data.Model;
using Social.Api.Infrastructure;

namespace Social.Api.Data
{
    public class SocialApiContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly TenantAccessor _tenantAccessor;
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AuditEntity> Auidits { get; set; }

        public SocialApiContext(DbContextOptions options, IConfiguration configuration, TenantAccessor tenantAccessor) :
            base(options)
        {
            _configuration = configuration;
            _tenantAccessor = tenantAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<PostEntity>()
                .HasOne<UserEntity>(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = _configuration.GetConnectionString("main");
            var connection = new SqlConnection(connectionString);
            connection.StateChange += ConnectionOnStateChange;
            options.UseSqlServer(connection);
        }

        private void ConnectionOnStateChange(object sender, StateChangeEventArgs e)
        {
            if (e.CurrentState == ConnectionState.Open && sender is SqlConnection connection)
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = @"exec sp_set_session_context @key=N'User', @value=@User";
                cmd.Parameters.AddWithValue("@User", _tenantAccessor.ActiveUser);
                cmd.ExecuteNonQuery();
            }
        }
    }
}