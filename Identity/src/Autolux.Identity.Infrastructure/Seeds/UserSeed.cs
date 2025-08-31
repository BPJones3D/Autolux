using Autolux.Identity.Domain.Users;

namespace Autolux.Identity.Infrastructure.Seeds;
public class UserSeed
{
    const string defaultPassword = "P@ssw0rd!";
    public static List<User> GetUsers()
    {
        var user1 = new User("bjoneswix@gmail.com", "Ben", "Jones", true);
        user1.SetPassword(defaultPassword);

        var user2 = new User("peter_jones_glass@hotmail.com", "Peter A", "Jones", true);
        user2.SetPassword(defaultPassword);

        return [user1, user2];
    }
}
