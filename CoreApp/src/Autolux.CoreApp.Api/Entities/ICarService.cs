using Autolux.CoreApp.Models.Cars;

namespace Autolux.CoreApp.Api.Entities;
public interface ICarService
{
    Task<CarModel> AddAsync(CarCreateModel carCreateModel, CancellationToken cancellationToken);
}
