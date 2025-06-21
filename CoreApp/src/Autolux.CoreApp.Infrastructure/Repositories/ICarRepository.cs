using Autolux.CoreApp.Domain.Cars;

namespace Autolux.CoreApp.Infrastructure.Repositories;
public interface ICarRepository
{
    Task<Car> AddAsync(Car car, CancellationToken cancellationToken);
}
