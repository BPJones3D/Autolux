using Autolux.CoreApp.Api.Setup;
using Autolux.CoreApp.Infrastructure;
using Autolux.CoreApp.Models.Cars;
using FluentValidation;

namespace Autolux.ApiGateway.Api.Setup;

public static class ApiGatewayServices
{
    public static void BindConfigurations(this IConfiguration configuration)
    {
        configuration.Bind(CoreDbSettings.CONFIG_NAME, CoreDbSettings.Instance);
    }

    public static async Task Initialize(this WebApplication app)
    {
        await app.Services.InitializeCoreAppDbAsync();
    }

    public static void InitializerFluentValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CarUpdateModel>, CarUpdateModelValidator>();
    }
}
