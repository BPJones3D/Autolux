namespace Autolux.Identity.Models.Roles;
public class UpdateRoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;

    public List<int> SelectedPermissionIds { get; set; } = default!;
}