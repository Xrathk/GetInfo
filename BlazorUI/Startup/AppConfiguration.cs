using Microsoft.Extensions.FileProviders;

namespace BlazorUI.Startup
{
    /// <summary>
    /// Sets up basic app properties for the GetInfo application.
    /// </summary>
    public static class AppConfiguration
    {

        /// <summary>
        /// Configures useful middleware for GetInfo application.
        /// </summary>
        /// <param name="app">Current web application</param>
        /// <returns>Configured web application</returns>
        public static WebApplication AddMiddleware(this WebApplication app)
        {
            app.UseRouting();
            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            // Returns new app configuration
            return app;
        }

        /// <summary>
        /// Configures static file serving for GetInfo application.
        /// </summary>
        /// <param name="app">Current web application</param>
        /// <param name="builder">Web application builder</param>
        /// <returns>Configured web application</returns>
        public static WebApplication AddStaticFiles(this WebApplication app, WebApplicationBuilder builder)
        {
            app.UseStaticFiles();
            // Serving static files from directory outside wwwroot
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                       Path.Combine(builder.Environment.ContentRootPath, "Resources/Pictures")),
                RequestPath = "/Pictures"
            });

            // Returns new app configuration
            return app;
        }

        /// <summary>
        /// Configures request pipeline for GetInfo application based on app environment.
        /// </summary>
        /// <param name="app">Current web application</param>
        /// <returns>Configured web application</returns>
        public static WebApplication ConfigureRequestPipeline(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Returns new app configuration
            return app;
        }
    }
}
