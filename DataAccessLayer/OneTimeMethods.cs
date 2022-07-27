using DataAccessLayer.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer
{
    /// <summary>
    /// Methods that get executed only once, depending on app needs.
    /// </summary>
    public class OneTimeMethods
    {

        // Fields
        private readonly GetInfoDbContext dbContext; // Db context
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<OneTimeMethods> Logger;

        // Service constructor
        public OneTimeMethods(IServiceScopeFactory _scopeFactory, ILogger<OneTimeMethods> logger)
        {
            scopeFactory = _scopeFactory;
            dbContext = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<GetInfoDbContext>();
            Logger = logger;
            Logger.LogInformation("GetInfo one-time database operations available.");

            // RUN ONLY ONCE - under here, put functions that must run only once for some operation in the database
            // For test environment (not applied in production environment)

        }

        /// <summary>
        /// This method hashes some passwords in the database (except those that were already hashed)
        /// </summary>
        public void HashPasswords()
        {
            var users = dbContext.AppUsers;
            foreach (var user in users)
            {
                if (user.Id < 6)
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                }

            }
            dbContext.SaveChanges();
            Logger.LogInformation("Passwords hashed: one-time operation.");
        }

    }
}
