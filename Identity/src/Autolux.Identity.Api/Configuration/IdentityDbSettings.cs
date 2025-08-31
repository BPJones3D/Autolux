namespace Autolux.Identity.Api.Configuration;

public class IdentityDbSettings
{
    public const string CONFIG_NAME = "IdentityDb";

    public static IdentityDbSettings Instance { get; } = new IdentityDbSettings();
    private IdentityDbSettings() { }
    public string ConnectionString { get; set; } = default!;
    public int? Timeout { get; set; } = default!;
}