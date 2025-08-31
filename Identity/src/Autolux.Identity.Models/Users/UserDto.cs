using Autolux.Identity.Models.Roles;

namespace Autolux.Identity.Models.Users;
public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsStaff { get; set; }

    public List<RoleNameDto> Roles { get; set; } = default!;
}