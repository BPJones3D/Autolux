namespace Autolux.Identity.Infrastructure.Seeds;
using Autolux.Identity.Domain.Users;
using Autolux.Identity.Infrastructure.Authentication;

public static class UserSeed
{
    public static List<User> GetGlobalAdminUsers(IPasswordHasher passwordHasher)
    {
        return
        [
            new("admin@test.com", passwordHasher.Hash("admin@test.com"), "Admin", "Test", "en-US", true)
        ];
    }
    public static List<User> GetDealerUsers(IPasswordHasher passwordHasher)
    {
        return
        [
            new("dealer@test.com", passwordHasher.Hash("dealer@test.com"), "Dealer", "Test", "en-US", true)
        ];
    }
}