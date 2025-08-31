using Autolux.Identity.Domain.Claims;

namespace Autolux.Identity.Domain.Users;

public class UserClaim
{
    private UserClaim() { }
    public UserClaim(Guid userId, Guid claimId)
    {
        if (userId == Guid.Empty) throw new ArgumentException($"{nameof(userId)} is required");
        if (claimId == Guid.Empty) throw new ArgumentException($"{nameof(claimId)} is required");
        UserId = userId;
        ClaimId = claimId;
    }

    public Guid UserId { get; private set; }
    public Guid ClaimId { get; private set; }
    public bool IsDeleted { get; private set; } = false;

    public DateTime Created { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; } = Guid.Empty;
    public DateTime Modified { get; set; } = DateTime.UtcNow;
    public Guid ModifiedBy { get; set; } = Guid.Empty;

    public User User { get; private set; } = default!;
    public Claim Claim { get; private set; } = default!;
}