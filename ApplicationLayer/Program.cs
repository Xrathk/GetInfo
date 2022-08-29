// Importing libraries
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Logging;
using DataAccessLayer.Data;
using DataAccessLayer;
using LogicLayer.Services;
using DataAccessLayer.Repositories;
using DataAccessLayer.Contacts;

// Initializing app
var builder = WebApplication.CreateBuilder(args);

// Add logging (serilog)
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
Log.Information("Setting up GetInfo application...");

// Add services to the container.
var connectionString = builder.Configuration.GetValue<string>("DbConnectionString");
builder.Services.AddDbContext<GetInfoDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Logging.AddProvider(new SerilogLoggerProvider()); // Add serilog logging

/// <summary>
/// Adding custom services
/// </summary>
// Repos
builder.Services.AddScoped<IRepositoryAppUser, RepositoryAppUser>();
builder.Services.AddScoped<IRepositoryAppUserSession, RepositoryAppUserSession>();
builder.Services.AddScoped<IRepositoryUserDetails, RepositoryUserDetails>();
builder.Services.AddScoped<IRepositoryAppUserRequests, RepositoryAppUserRequests>();
builder.Services.AddScoped<IRepositoryWeatherRequest, RepositoryWeatherRequest>();
// BLL Services (+ optional DAL service)
builder.Services.AddSingleton<ErrorCodes>(); // Add error codes
builder.Services.AddTransient<AccountOperations>(); // Add account operations service
builder.Services.AddTransient<WeatherApiOperations>(); // Add weather API operations service
if (builder.Environment.IsDevelopment()) // Add one-time operations service (not for production environment)
{
    builder.Services.AddSingleton<OneTimeMethods>(); 
}

var app = builder.Build(); // Building

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Serving static files from wwwroot
app.UseStaticFiles();
// Serving static files from directory outside wwwroot
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Resources/Pictures")),
    RequestPath = "/Pictures"
});

// Useful middleware
app.UseRouting();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

Log.Information("GetInfo setup successful. Starting app...");
try
{
    app.Run(); // Running app
}
catch (Exception ex)
{
    Log.Fatal("Application startup failed. Exception: {Ex}", ex); 
}
