namespace Autolux.Identity.Models.Roles;
public record RoleDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<RolePermissionDto> Permissions { get; init; } = [];
}
