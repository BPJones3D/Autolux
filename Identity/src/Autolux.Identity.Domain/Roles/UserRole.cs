using Autolux.Identity.Domain.Users;
using Autolux.SharedKernel.BaseClasses;

namespace Autolux.Identity.Domain.Roles;
public class UserRole : BaseEntity
{
    private UserRole() { }
    public UserRole(Guid userId, Guid roleId)
    {
        if (userId == Guid.Empty) throw new ArgumentException($"{nameof(userId)} is required");
        if (roleId == Guid.Empty) throw new ArgumentException($"{nameof(roleId)} is required");
        UserId = userId;
        RoleId = roleId;
    }

    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }

    public User User { get; private set; } = default!;
    public Role Role { get; private set; } = default!;
}