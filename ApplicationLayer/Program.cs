// Importing libraries
using Serilog;
using ApplicationLayer.Startup;

// Initializing app
var builder = WebApplication.CreateBuilder(args);

// Add logging 
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.AddLogging();

/// <summary>
/// Add services to application service container.
/// </summary>
Log.Information("Setting up GetInfo services...");

// Basic application services
builder.Services.RegisterBasicServices();

// Database context
builder.RegisterApplicationDbContext();

// Repos
builder.Services.RegisterRepos();

// BLL Services
builder.Services.RegisterBusinessLogicServices();

// Environment specific services
if (builder.Environment.IsDevelopment())
{
    builder.Services.ConfigureDebugServices();
}
else if (builder.Environment.IsProduction())
{
    builder.Services.ConfigureProductionServices();
}

// Build application
var app = builder.Build();

/// <summary>
/// Configure application
/// </summary>
Log.Information("Setting up GetInfo application...");

// HTTP request pipeline.
app.ConfigureRequestPipeline();

// Static files
app.AddStaticFiles(builder);

// Useful middleware
app.AddMiddleware();

// Setup ready - run application, exit if exception caught
Log.Information("GetInfo setup successful. Starting app...");
try
{
    app.Run(); 
}
catch (Exception ex)
{
    Log.Fatal("Application startup failed. Exception: {Ex}", ex); 
    Environment.Exit(-1); 
}
