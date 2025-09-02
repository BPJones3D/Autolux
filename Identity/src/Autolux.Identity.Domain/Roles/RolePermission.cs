using Autolux.Identity.Domain.Permissions;

namespace Autolux.Identity.Domain.Roles;
public class RolePermission
{
    public int PermissionId { get; private set; } = 0;
    public Permission Permission { get; private set; } = default!;

    public Guid RoleId { get; private set; } = Guid.Empty;
    public Role Role { get; private set; } = default!;

    private RolePermission() { }


    public RolePermission(Permission permission)
    {
        if (permission == null) throw new ArgumentException($"{nameof(permission)} is required");

        Permission = permission;
    }
}