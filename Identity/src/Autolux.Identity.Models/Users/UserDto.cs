using Autolux.Identity.Models.Roles;

namespace Autolux.Identity.Models.Users;
public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsStaff { get; set; }
    public string LanguageCode { get; set; } = string.Empty;

    public List<RoleDto> Roles { get; set; } = [];

    public bool IsAuthenticated => Id != Guid.Empty;
}
