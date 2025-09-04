using Autolux.Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Infrastructure.Respositories.Users;
public class UserRepository(IdentityDbContext dbContext) : IUserRepository
{

    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = await dbContext.Users
            .Where(x => x.Id == id)
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(email)) throw new ArgumentException($"{nameof(email)} is required");

        var user = await dbContext.Users
            .Where(x => x.NormalizedEmail == email.Normalize().ToUpperInvariant())
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<User> GetStaffByEmailAsync(string email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(email)) throw new ArgumentException($"{nameof(email)} is required");

        var user = await dbContext.Users
            .Where(x => x.IsStaff)
            .Where(x => x.NormalizedEmail == email.Normalize().ToUpperInvariant())
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(cancellationToken);

        return user;
    }

    public async Task<List<User>> GetListAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var count = await dbContext.Users.CountAsync();

        var data = await dbContext.Users
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .ToListAsync(cancellationToken);

        return data;
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task DeleteAsync(User user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}