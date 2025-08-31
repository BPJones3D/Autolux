using Autolux.Identity.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Autolux.Identity.Infrastructure.Setup;
public static class InfrastructureServices
{
    public static void AddIdentityInfrastructureServices(this IServiceCollection services)
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
    }
}
