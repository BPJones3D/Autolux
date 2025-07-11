using Autolux.CoreApp.Api.Setup;
using Autolux.CoreApp.Infrastructure;

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
}
