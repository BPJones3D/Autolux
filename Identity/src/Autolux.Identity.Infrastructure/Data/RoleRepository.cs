using Autolux.Identity.Domain.Contracts;
using Autolux.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Infrastructure.Data;
public class RoleRepository(IdentityDbContext dbContext) : IRoleRepository
{
    public async Task<Role> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = await dbContext.Roles
            .Where(x => x.Id == id)
            .Include(x => x.RolePermissions)
            .FirstOrDefaultAsync(cancellationToken);

        return role;
    }

    public async Task<Role> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} is required");

        var role = await dbContext.Roles
            .Where(x => x.NormalizedName == name.Normalize().ToUpperInvariant())
            .Include(x => x.RolePermissions)
            .FirstOrDefaultAsync(cancellationToken);

        return role;
    }

    public async Task<Role> GetByIdWithUsersAsync(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var role = await dbContext.Roles
            .Where(x => x.Id == id)
            .Include(x => x.RolePermissions)
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(cancellationToken);

        return role;
    }

    public async Task<Role> GetByNameWithUsersAsync(string name, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"{nameof(name)} is required");

        var role = await dbContext.Roles
            .Where(x => x.NormalizedName == name.Normalize().ToUpperInvariant())
            .Include(x => x.RolePermissions)
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.User)
            .FirstOrDefaultAsync(cancellationToken);

        return role;
    }

    public async Task<List<Role>> GetListAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var roles = await dbContext.Roles
            .ToListAsync(cancellationToken);

        return roles;
    }

    public async Task<List<Role>> GetListByIdAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var roles = await dbContext.Roles
            .Where(c => ids.Contains(c.Id))
            .ToListAsync(cancellationToken);

        return roles;
    }

    public async Task<List<Role>> GetListWithPermissionsAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var roles = await dbContext.Roles
        .Include(x => x.RolePermissions)
        .ToListAsync(cancellationToken);

        return roles;
    }

    public async Task<Role> AddAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        dbContext.Roles.Add(role);
        await dbContext.SaveChangesAsync(cancellationToken);

        return role;
    }

    public async Task<Role> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await dbContext.SaveChangesAsync(cancellationToken);

        return role;
    }

    public async Task DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        dbContext.Roles.Remove(role);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}