using Autolux.Identity.Api.Configuration;
using Autolux.Identity.Api.Services;
using Autolux.Identity.Domain.Contracts;
using Autolux.Identity.Infrastructure;
using Autolux.Identity.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Api.Setup;

public static class IdentityServices
{
    public static void AddIdentityServices(this IServiceCollection services)
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

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<IdentityDbInitializer>();

        /* Auto mapper used for mapping classes to DTO's, etc. */
        services.AddAutoMapper(cfg => { }, typeof(IdentityServices).Assembly);
    }
    public static async Task InitializeIdentity(this WebApplication app)
    {
        await app.Services.InitializeIdentityDbAsync();
    }
    public static async Task InitializeIdentityDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var identityDbInitializer = scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>();
        await identityDbInitializer.SeedAsync();
    }
}