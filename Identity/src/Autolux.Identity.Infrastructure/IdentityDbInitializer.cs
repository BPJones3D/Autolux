using Autolux.Identity.Infrastructure.Authentication;
using Autolux.Identity.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Infrastructure;
public class IdentityDbInitializer(IdentityDbContext dbContext, IPasswordHasher passwordHasher)
{
    /// <summary>
    /// Seed the identity database
    /// </summary>
    /// <param name="retry">Number of attempts to seed the database.</param>
    public async Task SeedAsync(int retry = 0)
    {
        await dbContext.Database.EnsureCreatedAsync();
        try
        {
            await SeedRoles();
            await SeedUsers();
        }
        catch
        {
            if (retry > 0)
            {
                await SeedAsync(retry - 1);
            }
        }
    }

    private async Task SeedRoles()
    {
        if (await dbContext.Roles.CountAsync() == 0)
        {
            dbContext.Roles.AddRange(RoleSeed.GetRoles());
            await dbContext.SaveChangesAsync();
        }
        else
        {
            // Since at this stage while working on the API we'll be adding new permissions continously
            // then we'll just re-add all permissions automatically to the globaladmin role.

            var globalAdminRole = await dbContext.Roles.Include(x => x.RolePermissions).FirstOrDefaultAsync(x => x.NormalizedName == RoleSeed.GlobalAdminRoleNameNormalized);
            if (globalAdminRole == null)
                throw new ArgumentException("GlobalAdmin role not found");

            /* NOTE: GlobalAdmin permissions are ALWAYS reset to ensure all permissions. You'll need to manually configure permissions for other roles */
            var permissions = PermissionSeed.GeneratePermissionsForAdmin();
            globalAdminRole.ClearAndAddPermissions(permissions);

            await dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedUsers()
    {
        if (await dbContext.Users.CountAsync() == 0)
        {
            var globalAdminRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.NormalizedName == RoleSeed.GlobalAdminRoleNameNormalized) ?? throw new ArgumentException("GlobalAdmin role not found");
            var users = UserSeed.GetGlobalAdminUsers(passwordHasher);

            dbContext.Users.AddRange(users);
            await dbContext.SaveChangesAsync();

            users.ForEach(x => x.AssignToRole(globalAdminRole.Id));
            await dbContext.SaveChangesAsync();
        }
    }
}