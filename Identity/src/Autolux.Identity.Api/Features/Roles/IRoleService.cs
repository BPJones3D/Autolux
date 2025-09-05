using Autolux.Identity.Models.Roles;

namespace Autolux.Identity.Api.Features.Roles;

public interface IRoleService
{
    Task<List<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken = default);
    Task<List<RoleDto>> GetListWithPermissionsAsync(CancellationToken cancellationToken);
}
