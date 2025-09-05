using Autolux.Identity.Infrastructure.Roles;
using Autolux.Identity.Models.Roles;
using AutoMapper;

namespace Autolux.Identity.Api.Features.Roles;

public class RoleService(IMapper mapper, IRoleRepository repository) : IRoleService
{
    public async Task<List<RoleDto>> GetAllRolesAsync(CancellationToken cancellationToken = default)
    {
        var roles = await repository.GetAllRolesAsync(cancellationToken);
        return mapper.Map<List<RoleDto>>(roles);
    }
    public async Task<List<RoleDto>> GetListWithPermissionsAsync(CancellationToken cancellationToken)
    {
        var roles = await repository.GetListWithPermissionsAsync(cancellationToken);

        return mapper.Map<List<RoleDto>>(roles);
    }
}
