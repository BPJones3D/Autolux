using Autolux.CoreApp.Domain.Cars;
using Microsoft.EntityFrameworkCore;

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

    public async Task DeleteAsync(Car car, CancellationToken cancellationToken)
    {
        var result = await _coreDbContext.Cars // find the "car" in the database using the id
            .Where(x => x.Id == car.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (result == null) // if the id wasnt found
            return;

        _coreDbContext.Cars.Remove(result); // remove the item from the database
        await _coreDbContext.SaveChangesAsync(cancellationToken); // save the database
    }

    public async Task<List<Car>> GetListByBrandAsync(string brand, CancellationToken cancellationToken)
    {
        if (brand == null)
            throw new ArgumentNullException(nameof(brand), "brand cannot be empty");

        return await _coreDbContext.Cars.Where(car => car.Brand == brand).ToListAsync(cancellationToken);
    }

    public async Task<Car> GetByIdAsync(Guid carId, CancellationToken cancellationToken)
    {
        if (carId == Guid.Empty)
            throw new ArgumentException(nameof(carId), "carId cannot be empty");

        var car = await _coreDbContext.Cars.FirstOrDefaultAsync(x => x.Id == carId, cancellationToken);

        return car ?? default!;
    }

    public async Task<List<Car>> GetListAsync(CancellationToken cancellationToken)
    {
        return await _coreDbContext.Cars.ToListAsync(cancellationToken);
    }

    public async Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken)
    {
        await _coreDbContext.SaveChangesAsync(cancellationToken);
        return car;
    }

    public async Task<List<Car>> GetListByYearAsync(int year, CancellationToken cancellationToken)
    {
        if (year < 1800)
            throw new ArgumentOutOfRangeException(nameof(year), "year must be 1800 or later");

        return await _coreDbContext.Cars.Where(car => car.Year == year).ToListAsync(cancellationToken);
    }

    public async Task<List<Car>> GetListByPriceBracketAsync(double low, double high, CancellationToken cancellationToken)
    {
        if (low < 0.00)
            throw new ArgumentException(nameof(low), "the price bracket cannot be lower than 0");

        if (high < low)
            throw new ArgumentException(nameof(high), "the high margin of the price bracket must be higher or equal to the low margin");

        return await _coreDbContext.Cars.Where(car => car.Price >= low && car.Price <= high).ToListAsync(cancellationToken);
    }
}
