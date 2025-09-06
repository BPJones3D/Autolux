using Autolux.ApiGateway.Api.Configuration.Authorization;
using Autolux.ApiGateway.Api.Setup;
using Autolux.ApiGateway.Api.Setup.Extensions;
using Autolux.CoreApp.Api.Setup;
using Autolux.CoreApp.Domain.Cars;
using Autolux.CoreApp.Infrastructure.Setup;
using Autolux.CoreApp.Models.Cars;
using Autolux.Identity.Api.Setup;
using FluentValidation.AspNetCore;
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

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<CarCreateModel, Car>();
    cfg.CreateMap<Car, CarModel>();
    cfg.CreateMap<Car, CarSummaryModel>();
});

builder.Services.AddSwaggerConfiguration();
builder.Services.AddInfrastructureServices();
builder.Services.AddCoreAppServices();
builder.Services.AddApiServices();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.InitializerFluentValidation();

// Authentication & Authorization
builder.Services.AddJwtAuthentication(configuration);
builder.Services.AddPolicies();
await builder.Services.AddIdentityServices();
builder.Services.AddApiGatewayServices();

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

app.UseAuthentication(); /***** HERE *****/
app.UseAuthorization();

app.MapControllers();

await app.Initialize();
await app.RunAsync();