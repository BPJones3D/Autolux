
using Autolux.Identity.Domain.Contracts;
using AutoMapper;

namespace Autolux.Identity.Api.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public UserService(IMapper mapper, IUserRepository userRepository, IRoleRepository roleRepository)
    {
        if (mapper is null)
        {
            throw new ArgumentException($"{nameof(mapper)} is null");
        }
        if (userRepository is null)
        {
            throw new ArgumentException($"{nameof(userRepository)} is null");
        }
        if (roleRepository is null)
        {
            throw new ArgumentException($"{nameof(roleRepository)} is null");
        }

        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
