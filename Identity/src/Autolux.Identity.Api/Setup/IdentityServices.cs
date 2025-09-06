using Autolux.Identity.Api.Features.Roles;
using Autolux.Identity.Api.Features.Users;
using Autolux.Identity.Infrastructure;
using Autolux.Identity.Infrastructure.Authentication;
using Autolux.Identity.Infrastructure.Configuration;
using Autolux.Identity.Infrastructure.Respositories.Users;
using Autolux.Identity.Infrastructure.Roles;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Autolux.Identity.Api.Setup;

public static class IdentityServices
{
    public static async Task AddIdentityServices(this IServiceCollection services)
    {
        var identityDbSettings = IdentityDbSettings.Instance;
        services.AddDbContext<IdentityDbContext>(options =>
        {
            options.UseSqlServer(identityDbSettings.ConnectionString, sqlServerOptions =>
            {
                if (identityDbSettings.Timeout is not null)
                {
                    sqlServerOptions.CommandTimeout(identityDbSettings.Timeout.Value);
                }
            });
        });

        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IdentityDbInitializer>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<RoleMappingProfile>();
            cfg.AddProfile<UserMappingProfile>();
        }, Assembly.GetExecutingAssembly());

        await InitializeIdentityDbAsync(services);
    }

    public static async Task InitializeIdentityDbAsync(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();

        var identityDbInitializer = scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>();
        await identityDbInitializer.SeedAsync();
    }
}

