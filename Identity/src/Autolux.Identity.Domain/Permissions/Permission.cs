using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Permissions;
public class Permission
{
    private Permission() { }

    public PermissionKey Key { get; private set; } = default!;
    public bool Value { get; private set; } = default!;

    public Permission(PermissionKey key, bool value = false)
    {
        Key = key;
        Value = value;
    }
}
