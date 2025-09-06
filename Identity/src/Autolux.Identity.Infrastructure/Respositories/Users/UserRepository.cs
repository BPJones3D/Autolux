using Autolux.Identity.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Autolux.Identity.Infrastructure.Respositories.Users;
public class UserRepository(IdentityDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrEmpty(email)) throw new ArgumentException($"{nameof(email)} is required");

        var user = await dbContext.Users
            .Where(x => x.NormalizedEmail == email.Normalize().ToUpperInvariant())
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.RolePermissions)
                        .ThenInclude(x => x.Permission)
            .FirstOrDefaultAsync(cancellationToken);

        return user;
    }
}