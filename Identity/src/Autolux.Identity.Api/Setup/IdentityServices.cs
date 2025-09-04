using Autolux.Identity.Api.Roles;
using Autolux.Identity.Infrastructure;
using Autolux.Identity.Infrastructure.Roles;
using System.Reflection;

namespace Autolux.Identity.Api.Setup;

public static class IdentityServices
{
    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IdentityDbInitializer>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<RoleMappingProfile>();
        }, Assembly.GetExecutingAssembly());
    }
    public static async Task InitializeIdentityDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var identityDbInitializer = scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>();
        await identityDbInitializer.SeedAsync();
    }
}