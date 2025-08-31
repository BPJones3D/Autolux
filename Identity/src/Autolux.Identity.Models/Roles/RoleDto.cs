using Autolux.Identity.Models.Permissions;

namespace Autolux.Identity.Models.Roles;
public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public List<PermissionDto> Permissions { get; set; } = default!;
}