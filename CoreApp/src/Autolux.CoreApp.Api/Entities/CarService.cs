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

    public async Task DeleteAsync(CarDeleteModel carDeleteModel, CancellationToken cancellationToken)
    {
        var carEntity = new Car(carDeleteModel.Id);

        await _carRepository.DeleteAsync(carEntity, cancellationToken);
    }

    public async Task<List<CarModel>> GetListByBrandAsync(string brand, CancellationToken cancellationToken)
    {
        if (brand == null)
            throw new ArgumentNullException(nameof(brand), "car brand cannot be empty");

        var cars = await _carRepository.GetListByBrandAsync(brand, cancellationToken);

        var carModels = new List<CarModel>();

        foreach (var car in cars)
        {
            var carModel = new CarModel()
            {
                Id = car.Id,
                Brand = car.Brand,
                Name = car.Name,
                Year = car.Year,
                Price = car.Price
            };

            carModels.Add(carModel);
        }

        return carModels;
    }

    public async Task<CarModel> GetByIdAsync(Guid carId, CancellationToken cancellationToken)
    {
        if (carId == Guid.Empty)
            throw new ArgumentException(nameof(carId), "carId cannot be empty");

        var car = await _carRepository.GetByIdAsync(carId, cancellationToken);

        if (car == null)
            return new CarModel();

        var carModel = new CarModel()
        {
            Id = car.Id,
            Brand = car.Brand,
            Name = car.Name,
            Year = car.Year,
            Price = car.Price
        };

        return carModel;
    }

    public async Task<List<CarSummaryModel>> GetSummaryListAsync(CancellationToken cancellationToken)
    {
        var cars = await _carRepository.GetListAsync(cancellationToken);

        var summaryList = cars.Select(car => new CarSummaryModel
        {
            Id = car.Id,
            Brand = car.Brand,
            Name = car.Name,
            Year = car.Year,
            Price = car.Price
        }).ToList();

        return summaryList;
    }

    public async Task<CarModel> UpdateAsync(CarUpdateModel carUpdateModel, CancellationToken cancellationToken)
    {
        if (carUpdateModel is null)
            throw new ArgumentNullException(nameof(carUpdateModel), "carUpdateModel is null");

        if (carUpdateModel.Id == Guid.Empty)
            throw new ArgumentException("carUpdateModel Id is empty", nameof(carUpdateModel));

        var car = await _carRepository.GetByIdAsync(carUpdateModel.Id, cancellationToken);

        if (car == null)
            throw new ArgumentException("Car not found");

        car.UpdateBrand(carUpdateModel.Brand);
        car.UpdateName(carUpdateModel.Name);
        car.UpdateYear(carUpdateModel.Year);
        car.UpdatePrice(carUpdateModel.Price);

        await _carRepository.UpdateAsync(car, cancellationToken);

        return new CarModel()
        {
            Id = car.Id,
            Brand = car.Brand,
            Name = car.Name,
            Year = car.Year
        };

    }

    public async Task<List<CarModel>> GetListByYearAsync(int year, CancellationToken cancellationToken)
    {
        if (year < 1800)
            throw new ArgumentOutOfRangeException(nameof(year), "year must be 1800 or higher");

        var cars = await _carRepository.GetListByYearAsync(year, cancellationToken);

        var carModels = new List<CarModel>();

        foreach (var car in cars)
        {
            var carModel = new CarModel()
            {
                Id = car.Id,
                Brand = car.Brand,
                Name = car.Name,
                Year = car.Year,
                Price = car.Price,
            };

            carModels.Add(carModel);
        }

        return carModels;
    }

    public async Task<List<CarModel>> GetListByPriceBracketAsync(double low, double high, CancellationToken cancellationToken)
    {
        if (low < 0.00)
            throw new ArgumentException(nameof(low), "the price bracket cannot be lower than 0");

        if (high < low)
            throw new ArgumentException(nameof(high), "the high margin of the price bracket must be higher or equal to the low margin");

        var cars = await _carRepository.GetListByPriceBracketAsync(low, high, cancellationToken);

        var carModels = new List<CarModel>();

        foreach (var car in cars)
        {
            var carModel = new CarModel()
            {
                Id = car.Id,
                Brand = car.Brand,
                Name = car.Name,
                Year = car.Year,
                Price = car.Price
            };

            carModels.Add(carModel);
        }

        return carModels;
    }
}
