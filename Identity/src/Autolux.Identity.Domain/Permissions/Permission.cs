using Autolux.SharedKernel.BaseClasses;
using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Permissions;
public class Permission : BaseEntity
{
    public PermissionKey PermissionKey { get; private set; } = default!;
    public bool Value { get; private set; } = default!;

    private Permission() { }
    public Permission(PermissionKey permissionKey, bool value = false)
    {
        Update(permissionKey, value);
    }

    public void Update(PermissionKey permissionKey, bool value = false)
    {
        if (permissionKey == null) throw new ArgumentException($"{nameof(permissionKey)} is required");

        PermissionKey = permissionKey;
        Value = value;
    }
}
