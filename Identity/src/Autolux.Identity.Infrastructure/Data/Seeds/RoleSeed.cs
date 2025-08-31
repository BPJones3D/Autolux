using Autolux.Identity.Domain.Entities;

namespace Autolux.Identity.Infrastructure.Data.Seeds;
public static class RoleSeed
{
    public static string GlobalAdminRoleNameNormalized { get => "GlobalAdmin".Normalize().ToUpperInvariant(); }

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
