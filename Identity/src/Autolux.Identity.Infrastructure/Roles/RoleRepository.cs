using Autolux.Identity.Domain.Roles;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Infrastructure.Roles;
public class RoleRepository(IdentityDbContext dbContext) : IRoleRepository
{
    public async Task<List<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Roles
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
