using Autolux.Identity.Domain.Users;
using Autolux.SharedKernel.BaseClasses;

namespace Autolux.Identity.Domain.Roles;
public class Role : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string NormalizedName { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public IEnumerable<UserRole> UserRoles => _userRoles.AsEnumerable();
    private readonly List<UserRole> _userRoles = new List<UserRole>();

    public IEnumerable<UserClaim> UserClaims => _userClaim.AsEnumerable();
    private readonly List<UserClaim> _userClaim = new List<UserClaim>();
}
