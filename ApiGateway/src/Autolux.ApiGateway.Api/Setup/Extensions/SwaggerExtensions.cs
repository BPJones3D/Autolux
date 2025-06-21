using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Autolux.ApiGateway.Api.Setup.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwaggerConfigurations(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Autolux ApiGateway Api",
                    Version = "v1",
                    Description = "Entry points for the Autolux ApiGateway Api",
                });

            swagger.EnableAnnotations();
            swagger.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
        });
    }

    public static void UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(swagger =>
        {
            swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "Autolux ApiGateway Api");
            swagger.RoutePrefix = "api/documentation";
        });
    }
}
