using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Permissions;
public class Permission
{
    public int Id { get; private set; }
    public bool Value { get; private set; }

    private Permission() { }
    public Permission(PermissionKey permissionKey, bool value = false)
    {
        Id = permissionKey.Value;
        Value = value;
    }
}
