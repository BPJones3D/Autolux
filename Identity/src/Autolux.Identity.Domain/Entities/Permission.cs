using Autolux.SharedKernel.BaseClasses;
using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Entities;
public class Permission : BaseEntity
{
    private Permission() { }
    public Permission(PermissionKey key, bool value = false)
    {
        Key = key;
        Value = value;
    }
    public PermissionKey Key { get; private set; }
    public bool Value { get; private set; }
}