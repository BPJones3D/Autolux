using Autolux.Identity.Domain.Entities;
using Autolux.SharedKernel.SharedObjects;

namespace Autolux.Identity.Infrastructure.Data.Seeds;
public static class PermissionSeed
{
    public static List<Permission> GeneratePermissionsForAdmin()
    {
        var permissions = new List<Permission>();

        foreach (var permissionKey in PermissionKey.List.OrderBy(x => x.Value))
        {
            permissions.Add(new Permission(permissionKey, true));
        }

        return permissions;
    }

    public static List<Permission> GeneratePermissions(IEnumerable<int> selectedPermissionIds)
    {
        var permissions = new List<Permission>();

        foreach (var permissionKey in PermissionKey.List.OrderBy(x => x.Value))
        {
            permissions.Add(new Permission(permissionKey, selectedPermissionIds.Contains(permissionKey.Value)));
        }

        return permissions;
    }
}
