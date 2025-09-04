namespace Autolux.Identity.Models.Users;
public record AuthenticationModel
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}
