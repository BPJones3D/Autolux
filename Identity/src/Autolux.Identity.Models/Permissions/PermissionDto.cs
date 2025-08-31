namespace Autolux.Identity.Models.Permissions;
public class PermissionDto
{
    public int Id { get; set; } = default!;
    public bool Value { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Group { get; set; } = default!;
}