namespace Autolux.Identity.Models.Roles;
public record RoleDto
{
    public Guid id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string NormalizedName { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<RolePermissionDto> Permissions { get; init; } = [];
}
