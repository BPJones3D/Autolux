using Autolux.Identity.Domain.Permissions;
using Autolux.Identity.Infrastructure.Authentication;
using Autolux.Identity.Infrastructure.Seeds;
using Autolux.SharedKernel.SharedObjects;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Infrastructure;
public class IdentityDbInitializer(IdentityDbContext dbContext, IPasswordHasher passwordHasher)
{
    public async Task SeedAsync()
    {
        await dbContext.Database.EnsureCreatedAsync();
        await SeedRoles();
        await SeedUsers();
    }

    private async Task SeedRoles()
    {
        if (!await dbContext.Roles.AnyAsync())
        {
            var roles = RoleSeed.GetRoles();
            foreach (var role in roles.Where(r => r.NormalizedName != RoleSeed.GlobalAdminRoleNameNormalized))
            {
                var permissions = new List<Permission>() { new(PermissionKey.CustomerRead, true), new(PermissionKey.VehicleRead, true) };
                role.ClearAndAddPermissions(permissions);
            }
            dbContext.Roles.AddRange(roles);
            await dbContext.SaveChangesAsync();
        }
        else
        {
            /* NOTE: GlobalAdmin permissions are ALWAYS reset to ensure all permissions. You'll need to manually configure permissions for other roles */
            var globalAdminRole = await dbContext.Roles.Include(x => x.RolePermissions).FirstOrDefaultAsync(x => x.NormalizedName == RoleSeed.GlobalAdminRoleNameNormalized)
                ?? throw new ArgumentException("GlobalAdmin role not found");

            var permissions = PermissionSeed.GeneratePermissionsForAdmin();
            globalAdminRole.ClearAndAddPermissions(permissions);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedUsers()
    {
        if (!await dbContext.Users.AnyAsync())
        {
            var roles = await dbContext.Roles.ToListAsync();

            var globalAdminRole = roles.FirstOrDefault(x => x.NormalizedName == RoleSeed.GlobalAdminRoleNameNormalized) ?? throw new ArgumentException("GlobalAdmin role not found");
            var users = UserSeed.GetGlobalAdminUsers(passwordHasher);
            users.ForEach(x => x.AssignToRole(globalAdminRole.Id));

            var dealerRole = roles.FirstOrDefault(x => x.NormalizedName == "DEALER");
            if (dealerRole != null)
            {
                var dealerUsers = UserSeed.GetDealerUsers(passwordHasher);
                dealerUsers.ForEach(x => x.AssignToRole(dealerRole.Id));
                users = users.Concat(dealerUsers).ToList();
            }

            dbContext.Users.AddRange(users);
            await dbContext.SaveChangesAsync();
        }
    }
}