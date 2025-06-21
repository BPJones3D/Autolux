using Autolux.CoreApp.Domain.Cars;
using Autolux.CoreApp.Infrastructure.Repositories;
using Autolux.CoreApp.Models.Cars;

namespace Autolux.CoreApp.Api.Entities;
public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        ArgumentNullException.ThrowIfNull(carRepository);
        _carRepository = carRepository;
    }

    public async Task<CarModel> AddAsync(CarCreateModel carCreateModel, CancellationToken cancellationToken)
    {
        if (carCreateModel == null)
            throw new ArgumentNullException(nameof(carCreateModel));

        var carEntity = new Car(
            carCreateModel.Brand,
            carCreateModel.Name,
            carCreateModel.Year,
            carCreateModel.Price
            );

        await _carRepository.AddAsync(carEntity, cancellationToken);

        return new CarModel
        {
            Brand = carCreateModel.Brand,
            Name = carCreateModel.Name,
            Year = carCreateModel.Year,
            Price = carCreateModel.Price
        };
    }
}
