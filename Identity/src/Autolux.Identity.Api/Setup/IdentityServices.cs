using Autolux.Identity.Infrastructure;

namespace Autolux.Identity.Api.Setup;

public static class IdentityServices
{
    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IdentityDbInitializer>();
    }
    public static async Task InitializeIdentityDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var identityDbInitializer = scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>();
        await identityDbInitializer.SeedAsync();
    }
}