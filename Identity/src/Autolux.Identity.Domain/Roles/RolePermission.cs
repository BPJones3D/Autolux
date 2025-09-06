using Autolux.Identity.Domain.Permissions;
using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Roles;
public class RolePermission
{
    public int Id { get; private set; } = default!;
    public Permission Permission { get; set; } = default!;

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = default!;

    private RolePermission() { }
    public RolePermission(PermissionKey permissionKey, bool permissionValue) : this(new Permission(permissionKey, permissionValue))
    {
    }

    public RolePermission(Permission permission)
    {
        if (permission is null) throw new ArgumentException($"{nameof(permission)} is required");

        Permission = permission;
    }
}
