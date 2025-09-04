namespace Autolux.ApiGateway.Api.Models.Identity;

public class PermissionModel
{
    public int Id { get; set; }
    public bool Value { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Group { get; set; } = string.Empty;
}
