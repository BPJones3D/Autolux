using Autolux.Identity.Domain.Claims;
using Autolux.Identity.Domain.Roles;
using Autolux.SharedKernel.BaseClasses;

namespace Autolux.Identity.Domain.Users;
public class User : BaseEntity
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string NormalizedEmail { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public IEnumerable<UserRole> UserRoles => _userRoles.AsEnumerable();
    private readonly List<UserRole> _userRoles = [];

    public IEnumerable<Role> Roles => UserRoles.Select(x => x.Role);

    public IEnumerable<UserClaim> UserClaims => _userClaim.AsEnumerable();
    private readonly List<UserClaim> _userClaim = [];

    public IEnumerable<Claim> Claims => UserClaims.Select(x => x.Claim);

    public void SetPassword(string password)
    {
        Password = password;
    }
}
