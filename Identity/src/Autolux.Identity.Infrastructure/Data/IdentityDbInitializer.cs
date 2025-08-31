using Autolux.Identity.Infrastructure.Data.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Infrastructure.Data;
public class IdentityDbInitializer
{
    private readonly IdentityDbContext _dbContext;

    public IdentityDbInitializer(IdentityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
        await SeedRoles();
        await SeedUsers();
    }

    private async Task SeedRoles()
    {
        if (!await _dbContext.Roles.AnyAsync())
        {
            _dbContext.Roles.AddRange(RoleSeed.GetRoles());
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            // Since at this stage while working on the API we'll be adding new permissions continously
            // then we'll just re-add all permissions automatically to the globaladmin role.

            var globalAdminRole = await _dbContext.Roles.Include(x => x.RolePermissions).FirstOrDefaultAsync(x => x.NormalizedName == RoleSeed.GlobalAdminRoleNameNormalized);
            if (globalAdminRole == null)
                return;

            /* NOTE: GlobalAdmin permissions are ALWAYS reset to ensure all permissions. You'll need to manually configure permissions for other roles */
            var permissions = PermissionSeed.GeneratePermissionsForAdmin();

            globalAdminRole.ClearAndAddPermissions(permissions);

            await _dbContext.SaveChangesAsync();
        }
    }

    private async Task SeedUsers()
    {
        if (!await _dbContext.Users.AnyAsync())
        {
            var globalAdminRole = await _dbContext.Roles.FirstOrDefaultAsync(x => x.NormalizedName == RoleSeed.GlobalAdminRoleNameNormalized);
            if (globalAdminRole == null)
                return;

            var users = UserSeed.GetGlobalAdminUsers();

            _dbContext.Users.AddRange(users);
            await _dbContext.SaveChangesAsync();

            users.ForEach(x => x.AssignToRole(globalAdminRole.Id));
            await _dbContext.SaveChangesAsync();
        }
    }
}
