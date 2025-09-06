using Autolux.ApiGateway.Api.Configuration.Authorization;
using Autolux.CoreApp.Api.Setup;
using Autolux.CoreApp.Infrastructure;
using Autolux.CoreApp.Models.Cars;
using Autolux.Identity.Infrastructure.Configuration;
using FluentValidation;

namespace Autolux.ApiGateway.Api.Setup;

public static class ApiGatewayServices
{
    public static void BindConfigurations(this IConfiguration configuration)
    {
        configuration.Bind(ApiGatewaySettings.CONFIG_NAME, ApiGatewaySettings.Instance);
        configuration.Bind(CoreDbSettings.CONFIG_NAME, CoreDbSettings.Instance);
        configuration.Bind(IdentityDbSettings.CONFIG_NAME, IdentityDbSettings.Instance);
    }

    public static void AddApiGatewayServices(this IServiceCollection services)
    {
        services.AddSingleton<IPermissionValidator>(services => PermissionValidator.Instance);
    }

    public static async Task Initialize(this WebApplication app)
    {
        await app.Services.InitializeCoreAppDbAsync();
    }

    public static void InitializerFluentValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CarUpdateModel>, CarUpdateModelValidator>();
        services.AddScoped<IValidator<CarCreateModel>, CarCreateModelValidator>();
    }
}