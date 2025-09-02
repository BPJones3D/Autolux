using Autolux.Identity.Domain.Permissions;
using Autolux.SharedKernel.BaseClasses;
using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Roles;
public class RolePermission : BaseEntity
{
    public Guid RoleId { get; private set; }
    public PermissionKey PermissionKey { get; private set; }

    public Role Role { get; private set; } = default!;
    public Permission Permission { get; private set; } = default!;

    private RolePermission() { }


    public RolePermission(Guid roleId, PermissionKey permissionKey)
    {
        if (roleId == Guid.Empty) throw new ArgumentException($"{nameof(roleId)} is required");
        if (permissionKey == null) throw new ArgumentException($"{nameof(permissionKey)} is required");

        RoleId = roleId;
        PermissionKey = permissionKey;
    }
}