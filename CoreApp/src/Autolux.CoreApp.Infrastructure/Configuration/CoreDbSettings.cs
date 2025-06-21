namespace Autolux.CoreApp.Infrastructure;

public class CoreDbSettings
{
    public const string CONFIG_NAME = "CoreDb";

    public static CoreDbSettings Instance { get; } = new CoreDbSettings();
    private CoreDbSettings() { }
    public string ConnectionString { get; set; } = default!;
    public int? Timeout { get; set; } = default!;
}
