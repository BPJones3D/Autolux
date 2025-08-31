using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Entities;
public class RolePermission
{
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

    public int Id { get; private set; } = default!;
    public Permission Permission { get; set; }

    public Guid RoleId { get; private set; } = Guid.Empty;
    public Role Role { get; private set; } = default!;
}