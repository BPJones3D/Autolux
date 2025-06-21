using Autolux.CoreApp.Api.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Autolux.CoreApp.Api.Setup;
public static class ApiServices
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<ICarService, CarService>();
    }
}
