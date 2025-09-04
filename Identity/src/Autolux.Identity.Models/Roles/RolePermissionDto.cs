namespace Autolux.Identity.Models.Roles;
public record RolePermissionDto
{
    public Guid RoleId { get; init; }
    public int PermissionId { get; init; }
}
