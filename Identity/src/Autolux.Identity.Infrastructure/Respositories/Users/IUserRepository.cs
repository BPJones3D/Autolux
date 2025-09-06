using Autolux.Identity.Domain.Users;

namespace Autolux.Identity.Infrastructure.Respositories.Users;
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
