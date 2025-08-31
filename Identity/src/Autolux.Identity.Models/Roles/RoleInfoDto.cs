namespace Autolux.Identity.Models.Roles;
public class RoleInfoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}