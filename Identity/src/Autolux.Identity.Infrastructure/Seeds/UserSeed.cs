namespace Autolux.Identity.Infrastructure.Seeds;
using Autolux.Identity.Domain.Users;
using Autolux.Identity.Infrastructure.Authentication;

public static class UserSeed
{
    public static List<User> GetGlobalAdminUsers(IPasswordHasher passwordHasher)
    {
        return
        [
            new("bjoneswix@gmail.com", passwordHasher.Hash("bjoneswix@gmail.com"), "Ben", "Jones", "en-US", true),
            new("peter_jones_glass@hotmail.com", passwordHasher.Hash("peter_jones_glass@hotmail.com"), "Peter A", "Jones", "en-US", true)
        ];
    }
}