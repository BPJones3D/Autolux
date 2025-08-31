using Autolux.Identity.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Infrastructure;
public class IdentityDbInitializer(IdentityDbContext dbContext)
{
    public async Task SeedAsync()
    {
        await dbContext.Database.EnsureCreatedAsync();
        await SeedRoles();
        await SeedClaims();
        await SeedUsers();
    }

    private async Task SeedRoles()
    {
        if (!await dbContext.Roles.AnyAsync())
        {
            dbContext.Roles.AddRange(RoleSeed.GetRoles());
            await dbContext.SaveChangesAsync();
        }
    }
    private async Task SeedClaims()
    {
        if (!await dbContext.Claims.AnyAsync())
        {
            dbContext.Claims.AddRange(ClaimSeed.GenerateClaimsForAdmin());
            await dbContext.SaveChangesAsync();
        }
    }
    private async Task SeedUsers()
    {
        if (!await dbContext.Users.AnyAsync())
        {
            var globalAdminRole = await dbContext.Roles.FirstAsync(r => r.Name == RoleSeed.GlobalAdminRoleNameNormalized);
            if (globalAdminRole == null) throw new ArgumentException($"{nameof(globalAdminRole)} is required");

            var claims = await dbContext.Claims.ToListAsync();

            var users = UserSeed.GetUsers();
            foreach (var user in users)
            {
                user.SetPassword("P@ssw0rd!");
                user.AssignToRole(globalAdminRole.Id);
                user.AddClaims(claims.Select(x => x.Id).ToList());
            }

            dbContext.Users.AddRange(UserSeed.GetUsers());
            await dbContext.SaveChangesAsync();
        }
    }
}
