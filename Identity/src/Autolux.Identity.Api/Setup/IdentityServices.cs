using Autolux.Identity.Api.Features.Roles;
using Autolux.Identity.Api.Features.Users;
using Autolux.Identity.Infrastructure;
using Autolux.Identity.Infrastructure.Authentication;
using Autolux.Identity.Infrastructure.Respositories.Users;
using Autolux.Identity.Infrastructure.Roles;
using System.Reflection;

namespace Autolux.Identity.Api.Setup;

public static class IdentityServices
{
    public static void AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IdentityDbInitializer>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<RolePermissionMappingProfile>();
            cfg.AddProfile<RoleMappingProfile>();
            cfg.AddProfile<UserMappingProfile>();
        }, Assembly.GetExecutingAssembly());
    }
    public static async Task InitializeIdentityDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var identityDbInitializer = scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>();
        await identityDbInitializer.SeedAsync();
    }
}