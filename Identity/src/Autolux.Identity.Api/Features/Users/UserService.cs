using Autolux.Identity.Infrastructure.Authentication;
using Autolux.Identity.Infrastructure.Respositories.Users;
using Autolux.Identity.Models.Users;
using AutoMapper;

namespace Autolux.Identity.Api.Features.Users;

public class UserService(IMapper mapper, IUserRepository userRepository, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<UserDto> Authenticate(string username, string password, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(username, cancellationToken);
        if (user == null || !passwordHasher.Verify(password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var result = mapper.Map<UserDto>(user);
        return result;
    }
}
