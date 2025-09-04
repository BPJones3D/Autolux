namespace Autolux.Identity.Infrastructure.Seeds;
using Autolux.Identity.Domain.Users;

public static class UserSeed
{
    public static List<User> GetGlobalAdminUsers()
    {
        var users = new List<User>();

        users.Add(new User("bjoneswix@gmail.com", "Ben", "Jones", "en-US", true));
        users.Add(new User("peter_jones_glass@hotmail.com", "Peter A", "Jones", "en-US", true));

        return users;
    }
}