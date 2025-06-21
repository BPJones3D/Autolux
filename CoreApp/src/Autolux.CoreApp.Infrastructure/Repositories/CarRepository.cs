using Autolux.CoreApp.Domain.Cars;

namespace Autolux.CoreApp.Infrastructure.Repositories;
public class CarRepository : ICarRepository
{
    private readonly CoreDbContext _coreDbContext;

    public CarRepository(CoreDbContext coreDbContext)
    {
        _coreDbContext = coreDbContext;
    }

    public async Task<Car> AddAsync(Car car, CancellationToken cancellationToken)
    {
        await _coreDbContext.Cars.AddAsync(car, cancellationToken);
        await _coreDbContext.SaveChangesAsync();

        return car;
    }
}
