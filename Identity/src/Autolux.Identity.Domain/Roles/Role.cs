using Autolux.Identity.Domain.Permissions;
using Autolux.Identity.Domain.Users;
using Autolux.SharedKernel.BaseClasses;
using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Domain.Roles;
public class Role : BaseEntity
{
    private Role() { }

    public string Name { get; private set; } = default!;
    public string NormalizedName { get; private set; } = default!;
    public string Description { get; set; } = default!;

    public IEnumerable<UserRole> UserRoles => _userRoles.AsEnumerable();
    private readonly List<UserRole> _userRoles = new List<UserRole>();

    public IEnumerable<RolePermission> RolePermissions => _rolePermissions.AsEnumerable();
    private readonly List<RolePermission> _rolePermissions = new List<RolePermission>();

    public IEnumerable<User> Users => UserRoles.Select(x => x.User);
    public IEnumerable<Permission> SelectedPermissions => RolePermissions.Select(x => x.Permission).Where(x => x.Value);
    //public IEnumerable<Permission> Permissions => PermissionSeed.GeneratePermissions(SelectedPermissions.Select(x => x.Key.Value));
    //public IEnumerable<Permission> Permissions
    //{
    //    get
    //    {
    //        var permissions = new List<Permission>();
    //        foreach (var item in PermissionKey.List)
    //        {
    //            permissions.Add(new Permission(item, SelectedPermissions.Any(x => x.Key == item && x.Value)));
    //        }
    //        return permissions;
    //    }
    //}
    public IEnumerable<Permission> Permissions => SelectedPermissions.Select(x => x);

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

    public void AssignUser(Guid userId)
    {
        if (!UserRoles.Any(x => x.UserId == userId))
        {
            var userRole = new UserRole(userId, Id);
            _userRoles.Add(userRole);
        }
    }

    public void RemoveUser(Guid userId)
    {
        var userRole = _userRoles.Find(v => v.UserId == userId);

        if (userRole is null) return;

        _userRoles.Remove(userRole);
    }

    public void ClearAndAddPermissions(IEnumerable<int> permissionIds)
    {
        if (permissionIds is null) return;

        var permissions = permissionIds.Select(x => new Permission(PermissionKey.FromValue(x), true));

        ClearAndAddPermissions(permissions);
    }

    /// <summary>
    /// It clears the existing list of permissions, and adds the permissions given in the argument.
    /// It adds only permissions whith value true.
    /// </summary>
    /// <param name="permissions">Permissions to add to the role</param>
    public void ClearAndAddPermissions(IEnumerable<Permission> permissions)
    {
        if (permissions is null) return;

        _rolePermissions.Clear();

        if (permissions is not null)
        {
            _rolePermissions.AddRange(permissions.Where(x => x.Value == true).Select(x => new RolePermission(x)));
        }
    }
}
