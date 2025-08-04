using Autolux.ApiGateway.Api.Setup;
using Autolux.ApiGateway.Api.Setup.Extensions;
using Autolux.CoreApp.Api.Setup;
using Autolux.CoreApp.Infrastructure.Setup;
using Autolux.CoreApp.Models.Cars;
using FluentValidation;
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
builder.Services.AddCoreAppServices();
builder.Services.AddApiServices();
builder.Services.AddScoped<IValidator<CarUpdateModel>, CarUpdateModelValidator>();

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

await app.Initialize();
await app.RunAsync();