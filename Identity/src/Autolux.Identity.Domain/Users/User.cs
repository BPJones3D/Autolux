using Autolux.Identity.Domain.Claims;
using Autolux.Identity.Domain.Roles;
using Autolux.SharedKernel.BaseClasses;
using System.Net.Mail;

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

    public IEnumerable<UserClaim> UserClaims => _userClaims.AsEnumerable();
    private readonly List<UserClaim> _userClaims = [];

    public IEnumerable<Claim> Claims => UserClaims.Select(x => x.Claim);

    private User() { }

    public User(string email, string firstName, string lastName, bool isStaff = true)
    {
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException($"{nameof(email)} is required");

        var emailAddress = new MailAddress(email);
        Email = emailAddress.Address;
        NormalizedEmail = Email.Normalize().ToUpperInvariant();

        Update(firstName, lastName, isStaff);
    }
    public void Update(string firstName, string lastName, bool isStaff = true)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public void SetPassword(string password)
    {
        Password = password;
    }
    public void AssignToRole(Guid roleId)
    {
        if (!UserRoles.Any(x => x.RoleId == roleId))
        {
            var userRole = new UserRole(Id, roleId);
            _userRoles.Add(userRole);
        }
    }
    public void AssignToRoles(IEnumerable<Guid> roleIds)
    {
        if (roleIds is null) return;

        foreach (var roleId in roleIds)
        {
            AssignToRole(roleId);
        }
    }
    public void AddClaim(Guid claimId)
    {
        if (!UserClaims.Any(x => x.ClaimId == claimId))
        {
            var userClaim = new UserClaim(Id, claimId);
            _userClaims.Add(userClaim);
        }
    }
    public void AddClaims(IEnumerable<Guid> claimIds)
    {
        if (claimIds is null) return;

        foreach (var claimId in claimIds)
        {
            AddClaim(claimId);
        }
    }
}
