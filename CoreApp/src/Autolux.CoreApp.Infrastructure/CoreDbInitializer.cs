using Autolux.CoreApp.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Autolux.CoreApp.Infrastructure;
public class CoreDbInitializer(CoreDbContext _dbContext, IConfiguration conf)
{
    public async Task SeedAsync()
    {
        await SeedCars();
    }

    private async Task SeedCars()
    {
        if (!await _dbContext.Cars.AnyAsync())
        {
            var awardRules = CarSeed.GetCars();
            _dbContext.Cars.AddRange(awardRules);
            await _dbContext.SaveChangesAsync();
        }
    }
}
