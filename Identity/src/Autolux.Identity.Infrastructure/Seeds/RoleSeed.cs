using Autolux.Identity.Domain.Roles;

namespace Autolux.Identity.Infrastructure.Seeds;
public static class RoleSeed
{
    private static string _globalAdminRoleNameNormalized = "GlobalAdmin".Normalize().ToUpperInvariant();

    public static string GlobalAdminRoleNameNormalized => _globalAdminRoleNameNormalized;

    public static List<Role> GetRoles()
    {
        var permissions = PermissionSeed.GeneratePermissionsForAdmin();
        var globalAdminRole = new Role("GlobalAdmin", "Global admin role");
        globalAdminRole.ClearAndAddPermissions(permissions);

        return
            [
                globalAdminRole,
                new Role("Admin", "Admin role"),
                new Role("RetailerAdmin", "Retailer admin role"),
                new Role("Retailer", "Retailer role"),
            ];
    }
}