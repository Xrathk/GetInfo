using DataAccessLayer;
using DataAccessLayer.Contacts;
using DataAccessLayer.Data;
using DataAccessLayer.Repositories;
using LogicLayer.Services;
using Microsoft.EntityFrameworkCore;
using Serilog.Extensions.Logging;

namespace ApplicationLayer.Startup
{
    /// <summary>
    /// Sets up necessary services for the GetInfo application via dependency injection.
    /// </summary>
    public static class ServicesSetup
    {
        /// <summary>
        /// Adds basic services to the GetInfo service container.
        /// </summary>
        /// <param name="services">Application service container</param>
        /// <returns>The new service container</returns>
        public static IServiceCollection RegisterBasicServices(this IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Return new service container
            return services;
        }

        /// <summary>
        /// Adds repositories to the GetInfo service container.
        /// </summary>
        /// <param name="services">Application service container</param>
        /// <returns>The new service container</returns>
        public static IServiceCollection RegisterRepos(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryAppUser, RepositoryAppUser>();
            services.AddScoped<IRepositoryAppUserSession, RepositoryAppUserSession>();
            services.AddScoped<IRepositoryUserDetails, RepositoryUserDetails>();
            services.AddScoped<IRepositoryAppUserRequests, RepositoryAppUserRequests>();
            services.AddScoped<IRepositoryWeatherRequest, RepositoryWeatherRequest>();
            services.AddScoped<IRepositoryNewsRequest, RepositoryNewsRequest>();

            // Return new service container
            return services;
        }

        /// <summary>
        /// Adds business logic services to the GetInfo service container.
        /// </summary>
        /// <param name="services">Application service container</param>
        /// <returns>The new service container</returns>
        public static IServiceCollection RegisterBusinessLogicServices(this IServiceCollection services)
        {
            services.AddSingleton<ErrorCodes>(); // Add error codes
            services.AddTransient<AccountOperations>(); // Add account operations service
            services.AddTransient<WeatherApiOperations>(); // Add weather API operations service
            services.AddTransient<NewsApiOperations>(); // Add news API operations service

            // Return new service container
            return services;
        }

        /// <summary>
        /// Configures extra services for the debug environment.
        /// </summary>
        /// <param name="services">Application service container</param>
        /// <returns>The new service container</returns>
        public static IServiceCollection ConfigureDebugServices(this IServiceCollection services)
        {
            services.AddSingleton<OneTimeMethods>();

            // Return new service container
            return services;
        }

        /// <summary>
        /// Configures extra services for the production environment.
        /// </summary>
        /// <param name="services">Application service container</param>
        /// <returns>The new service container</returns>
        public static IServiceCollection ConfigureProductionServices(this IServiceCollection services)
        {
            /// TBD

            // Return new service container
            return services;
        }

        /// <summary>
        /// Registers database contexts to the GetInfo service container.
        /// </summary>
        /// <param name="builder">Application builder</param>
        /// <returns>The new application builder</returns>
        public static WebApplicationBuilder RegisterApplicationDbContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetValue<string>("DbConnectionString");
            builder.Services.AddDbContext<GetInfoDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Return new application builder
            return builder;
        }

        /// <summary>
        /// Registers logging services to the GetInfo service container.
        /// </summary>
        /// <param name="builder">Application builder</param>
        /// <returns>The new application builder</returns>
        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
        {
            builder.Logging.AddProvider(new SerilogLoggerProvider()); // Serilog logging

            // Return new application builder
            return builder;
        }
    }
}
