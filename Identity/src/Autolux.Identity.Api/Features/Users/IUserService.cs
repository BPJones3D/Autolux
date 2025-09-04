using Autolux.Identity.Models.Users;

namespace Autolux.Identity.Api.Features.Users;

public interface IUserService
{
    Task<UserDto> Authenticate(string username, string password, CancellationToken cancellationToken);
}