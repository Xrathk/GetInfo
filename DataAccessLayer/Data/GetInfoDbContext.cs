using Microsoft.EntityFrameworkCore;
using DomainLayer.Entities;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Data
{
    /// <summary>
    /// Db context class for GetInfo app.
    /// </summary>
    public class GetInfoDbContext : DbContext
    {

        public static string connectionString; // DB connection string

        /// <summary>
        /// GetInfo database context constructor.
        /// </summary>
        /// <param name="config">App configuration (for getting database connection string)</param>
        public GetInfoDbContext(IConfiguration config)
        {
            connectionString = config.GetValue<string>("DbConnectionString");

            // RUN ONLY ONCE - under here, put functions that must run only once for some operation in the database
            // For test environment (not applied in production environment)

        }


        /// <summary>
        /// Database configuration override function.
        /// </summary>
        /// <param name="optionsBuilder">DbContext options</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }


        // Database tables
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppUserSession> AppUserSessions { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<AppUserRequests> AppUserRequestsOverview { get; set; }
        public DbSet<WeatherRequest> WeatherRequests { get; set; }


        /// <summary>
        /// Database entry operation specifics.
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary keys
            modelBuilder.Entity<AppUser>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<AppUserSession>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<UserDetails>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<AppUserRequests>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<WeatherRequest>()
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

