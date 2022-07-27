using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Models.Entities;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Data
{
    /// <summary>
    /// Db context class for GetInfo app.
    /// </summary>
    public class GetInfoDbContext : DbContext
    {

        public static string connectionString;

        // Getting connection string from configuration file
        public GetInfoDbContext(IConfiguration config)
        {
            connectionString = config.GetValue<string>("DbConnectionString");

            // RUN ONLY ONCE - under here, put functions that must run only once for some operation in the database
            // For test environment (not applied in production environment)

        }


        // Configuring database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }


        // Database tables
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserSession> AppUserSessions { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }


        // Entry CRUD operation specifics
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary keys
            modelBuilder.Entity<AppUser>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<AppUserSession>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<UserDetails>()
                .HasKey(p => p.Id);

            /*
            // Nav properties
            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.UserDetails)
                .WithOne(b => b.AppUser);

            modelBuilder.Entity<AppUser>()
                .HasOne(a => a.UserSession)
                .WithOne(b => b.AppUser);
            */
        }

    }
}

