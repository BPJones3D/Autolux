
using Autolux.Identity.Domain.Contracts;
using AutoMapper;

namespace Autolux.Identity.Api.Services;

public class RoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IRoleRepository _roleRepository;

    public RoleService(IMapper mapper, IRoleRepository roleRepository)
    {
        if (mapper is null)
        {
            throw new ArgumentException($"{nameof(mapper)} is null");
        }
        if (roleRepository is null)
        {
            throw new ArgumentException($"{nameof(roleRepository)} is null");
        }

        _mapper = mapper;
        _roleRepository = roleRepository;
    }
    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
