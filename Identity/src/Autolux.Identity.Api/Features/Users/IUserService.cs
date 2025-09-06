namespace Autolux.Identity.Api.Features.Users;

public interface IUserService
{
    Task<string> Authenticate(string username, string password, string jwtEncryptionKey, CancellationToken cancellationToken);
}