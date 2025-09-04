using Autolux.Identity.Domain.Users;
using Autolux.SharedKernel.BaseClasses;

namespace Autolux.Identity.Domain.Roles;
public class UserRole : BaseEntityNoId
{
    private UserRole() { }

    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public User User { get; private set; } = default!;
    public Role Role { get; private set; } = default!;

    public UserRole(Guid userId, Guid roleId)
    {
        // Do not check for Guid.Empty, EF will recognize it as a new record.
        UserId = userId;
        RoleId = roleId;
    }
}