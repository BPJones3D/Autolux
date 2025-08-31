using Autolux.SharedKernel.BaseClasses;
using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Claims;

public class Claim : BaseEntity
{
    private Claim() { }
    public Claim(PermissionKey key, bool value = false)
    {
        Key = key;
        Value = value;
    }
    public PermissionKey Key { get; private set; }
    public bool Value { get; private set; }
}