namespace Autolux.ApiGateway.Api;

public class ApiGatewaySettings
{
    public const string CONFIG_NAME = "ApiGateway";

    public static ApiGatewaySettings Instance { get; } = new ApiGatewaySettings();
    private ApiGatewaySettings() { }
    public string JWTEncryptionKey { get; set; } = default!;
}
