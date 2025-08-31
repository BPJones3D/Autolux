namespace Autolux.Identity.Models.Users;
public class CreateUserDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsStaff { get; set; }
    public string LanguageCode { get; set; } = default!;

    public List<Guid> RoleIds { get; set; } = default!;
}