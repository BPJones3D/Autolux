using Autolux.CoreApp.Infrastructure;
using Autolux.CoreApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class InfrastructureServices
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        var coreDbSettings = CoreDbSettings.Instance;
        services.AddDbContext<CoreDbContext>(options =>
        {
            options.UseSqlServer(coreDbSettings.ConnectionString, sqlServerOptions =>
            {
                if (coreDbSettings.Timeout is not null)
                {
                    sqlServerOptions.CommandTimeout(coreDbSettings.Timeout.Value);
                }
            });
        });

        services.AddScoped<ICarRepository, CarRepository>();
    }
}
