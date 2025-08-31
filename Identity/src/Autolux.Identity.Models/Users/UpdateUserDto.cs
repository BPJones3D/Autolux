namespace Autolux.Identity.Models.Users;
public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public bool IsStaff { get; set; }

    public List<Guid> RoleIds { get; set; } = default!;
}