using Autolux.Identity.Models.Roles;

namespace Autolux.Identity.Api.Roles;

public interface IRoleService
{
    Task<List<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    //Task UpdateAsync(CancellationToken cancellationToken = default);
}
