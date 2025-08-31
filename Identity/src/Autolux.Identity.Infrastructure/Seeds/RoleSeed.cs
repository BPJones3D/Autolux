using Autolux.Identity.Domain.Roles;

namespace Autolux.Identity.Infrastructure.Seeds;
public static class RoleSeed
{
    public static string GlobalAdminRoleNameNormalized { get => "GlobalAdmin".Normalize().ToUpperInvariant(); }

    public static List<Role> GetRoles()
    {
        return
            [
                new Role(GlobalAdminRoleNameNormalized, "Global admin role"),
                new Role("Admin", "Admin role"),
                new Role("RetailerAdmin", "Retailer admin role"),
                new Role("Retailer", "Retailer role"),
            ];
    }
}