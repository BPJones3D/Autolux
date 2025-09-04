namespace Autolux.ApiGateway.Api.Models.Identity;

public class RoleModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public List<PermissionModel> Permissions { get; set; } = [];
}
