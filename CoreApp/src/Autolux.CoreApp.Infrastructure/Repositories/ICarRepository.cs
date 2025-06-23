using Autolux.CoreApp.Domain.Cars;

namespace Autolux.CoreApp.Infrastructure.Repositories;
public interface ICarRepository
{
    Task<Car> AddAsync(Car car, CancellationToken cancellationToken);
    Task DeleteAsync(Car car, CancellationToken cancellationToken);
    Task<Car> UpdateAsync(Car car, CancellationToken cancellationToken);
    Task<Car> GetByIdAsync(Guid carId, CancellationToken cancellationToken);
    Task<List<Car>> GetListAsync(CancellationToken cancellationToken);
    Task<List<Car>> GetListByBrandAsync(string brand, CancellationToken cancellationToken);
    Task<List<Car>> GetListByYearAsync(int year, CancellationToken cancellationToken);
    Task<List<Car>> GetListByPriceBracketAsync(double low, double high, CancellationToken cancellationToken);

}
