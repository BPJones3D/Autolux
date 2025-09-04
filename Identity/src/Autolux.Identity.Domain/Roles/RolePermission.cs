using Autolux.Identity.Domain.Permissions;
using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Roles;
public class RolePermission
{
    public int Id { get; private set; }
    public Permission Permission { get; set; }

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; }

    private RolePermission() { }
    public RolePermission(PermissionKey permissionKey, bool permissionValue)
    {
        Permission = new Permission(permissionKey, permissionValue);
    }

    public RolePermission(Permission permission)
    {
        if (permission is null) throw new ArgumentException($"{nameof(permission)} is required");

        Permission = permission;
    }
}
