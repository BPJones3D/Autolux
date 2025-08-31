namespace Autolux.Identity.Models.Roles;
public class CreateRoleDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public List<int> SelectedPermissionIds { get; set; } = default!;
}