using Autolux.CoreApp.Infrastructure;

public static class ApiGatewayServices
{
    public static void BindConfigurations(this IConfiguration configuration)
    {
        configuration.Bind(CoreDbSettings.CONFIG_NAME, CoreDbSettings.Instance);
    }
}
