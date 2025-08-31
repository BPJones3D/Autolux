using Autolux.Identity.Domain.Users;

namespace Autolux.Identity.Domain.Roles;
public class UserRole
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
    public bool IsDeleted { get; private set; } = false;

    public DateTime Created { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    public Guid ModifiedBy { get; set; } = Guid.Empty;

    public User User { get; private set; } = default!;
    public Role Role { get; private set; } = default!;
}