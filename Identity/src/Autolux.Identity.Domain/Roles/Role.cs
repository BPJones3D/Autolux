using Autolux.Identity.Domain.Permissions;
using Autolux.SharedKernel.BaseClasses;

namespace Autolux.Identity.Domain.Roles;
public class Role : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string NormalizedName { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public IEnumerable<UserRole> UserRoles => _userRoles.AsEnumerable();
    private readonly List<UserRole> _userRoles = [];

    public IEnumerable<RolePermission> RolePermissions => _rolePermissions.AsEnumerable();
    private readonly List<RolePermission> _rolePermissions = [];

    private Role() { }
    public Role(string name, string description)
    {
        Update(name, description);
    }
    public void Update(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException($"{nameof(name)} is required");

        Name = name;
        NormalizedName = name.Normalize().ToUpperInvariant();
        Description = description;
    }
    public void ClearAndAddPermissions(IEnumerable<Permission>? permissions)
    {
        if (permissions is null) return;

        _rolePermissions.Clear();

        _rolePermissions.AddRange(permissions.Where(x => x.Value).Select(x => new RolePermission(x)));
    }
}
