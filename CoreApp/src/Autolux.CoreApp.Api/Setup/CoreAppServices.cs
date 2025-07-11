using Autolux.CoreApp.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Autolux.CoreApp.Api.Setup;
public static class CoreAppServices
{
    public static void AddCoreAppServices(this IServiceCollection services)
    {
        services.AddScoped<CoreDbInitializer>();
    }
    public static async Task InitializeCoreAppDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var coreDbInitializer = scope.ServiceProvider.GetRequiredService<CoreDbInitializer>();
        await coreDbInitializer.SeedAsync();
    }
}
