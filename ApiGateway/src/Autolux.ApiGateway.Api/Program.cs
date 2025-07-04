using Autolux.ApiGateway.Api.Setup.Extensions;
using Autolux.CoreApp.Api.Setup;
using System.Text.Json.Serialization;

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(AppContext.BaseDirectory))
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true);

// add and bind configurations
var configuration = configurationBuilder.Build();
configuration.BindConfigurations();

// add services on the container
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddSwaggerConfigurations();
builder.Services.AddInfrastructureServices();
builder.Services.AddApiServices();

//Configure the HTTP request pipeline
var app = builder.Build();
app.UseCors(policy =>
{
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
});
app.UseCustomSwagger();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();