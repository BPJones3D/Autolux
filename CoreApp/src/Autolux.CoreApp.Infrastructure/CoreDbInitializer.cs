using Autolux.CoreApp.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;

namespace Autolux.CoreApp.Infrastructure;
public class CoreDbInitializer(CoreDbContext _dbContext)
{
    public async Task SeedAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
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
