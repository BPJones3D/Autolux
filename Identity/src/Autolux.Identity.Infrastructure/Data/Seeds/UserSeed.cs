using Autolux.Identity.Domain.Entities;

namespace Autolux.Identity.Infrastructure.Data.Seeds;
public static class UserSeed
{
    public static List<User> GetGlobalAdminUsers()
    {
        var users = new List<User>
        {
            new("bjoneswix@gmail.com", "Ben", "Jones", true),
            new("peter_jones_glass@hotmail.com", "Peter A", "Jones", true)
        };

        return users;
    }
}
