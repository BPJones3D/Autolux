using Autolux.Identity.Domain.Roles;

namespace Autolux.Identity.Infrastructure.Roles;
public interface IRoleRepository
{
    Task<List<Role>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(CancellationToken cancellationToken = default);
}
