using Autolux.CoreApp.Domain.Cars;
using Autolux.CoreApp.Infrastructure.Repositories;
using Autolux.CoreApp.Models.Cars;
using AutoMapper;

namespace Autolux.CoreApp.Api.Entities;
public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public CarService(ICarRepository carRepository, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(carRepository);
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<CarModel> AddAsync(CarCreateModel carCreateModel, CancellationToken cancellationToken)
    {
        if (carCreateModel == null)
            throw new ArgumentNullException(nameof(carCreateModel));

        var carEntity = _mapper.Map<Car>(carCreateModel);

        var car = await _carRepository.AddAsync(carEntity, cancellationToken);

        return _mapper.Map<CarModel>(car);
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
            var carModel = _mapper.Map<CarModel>(car);

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

        var carModel = _mapper.Map<CarModel>(car);

        return carModel;
    }

    public async Task<List<CarSummaryModel>> GetSummaryListAsync(string filter, string filterOrder, CancellationToken cancellationToken)
    {
        var cars = await _carRepository.GetListAsync(cancellationToken);

        var filteredCars = cars;

        switch (filter?.ToLower())
        {
            case "price":
                filteredCars = cars.OrderBy(c => c.Price).ToList();
                break;

            case "year":
                filteredCars = cars.OrderBy(c => c.Year).ToList();
                break;

            case "miles":
                filteredCars = cars.OrderBy(c => c.Miles).ToList();
                filteredCars.RemoveAll(s => s.FuelType == "Electric");
                break;

            case "brand":
                filteredCars = cars.OrderBy(c => c.Brand).ToList();
                break;

            case "mpg":
                filteredCars = cars.OrderBy(c => c.MilesPerGallon).ToList();
                filteredCars.RemoveAll(s => s.FuelType == "Electric");
                break;

            case "tankcapacity":
                filteredCars = cars.OrderBy(c => c.TankCapacity).ToList();
                filteredCars.RemoveAll(s => s.FuelType == "Electric");
                break;

            case "evrange":
                filteredCars = cars.OrderBy(c => c.TankCapacity).ToList();
                filteredCars.RemoveAll(s => s.FuelType != "Electric");
                break;

            case "seats":
                filteredCars = cars.OrderBy(c => c.SeatCount).ToList();
                break;

            case "doors":
                filteredCars = cars.OrderBy(c => c.DoorCount).ToList();
                break;

            case "Relevancy":
                break;

            default:
                break;
        }

        if (filterOrder == "Ascending")
        {
            filteredCars.Reverse();
        }

        var summaryList = _mapper.Map<List<CarSummaryModel>>(filteredCars);

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
        car.UpdateMiles(carUpdateModel.Miles);
        car.UpdateTransmission(carUpdateModel.Transmission);
        car.UpdateFuelType(carUpdateModel.FuelType);
        car.UpdateTankCapacity(carUpdateModel.TankCapacity);
        car.UpdateMilesPerGallon(carUpdateModel.MilesPerGallon);
        car.UpdateSeatCount(carUpdateModel.SeatCount);
        car.UpdateDoorCount(carUpdateModel.DoorCount);
        car.UpdateColour(carUpdateModel.Colour);
        car.UpdateDescription(carUpdateModel.Description);

        await _carRepository.UpdateAsync(car, cancellationToken);


        return _mapper.Map<CarModel>(car);

    }

    public async Task<List<CarModel>> GetListByYearAsync(int year, CancellationToken cancellationToken)
    {
        if (year < 1800)
            throw new ArgumentOutOfRangeException(nameof(year), "year must be 1800 or higher");

        var cars = await _carRepository.GetListByYearAsync(year, cancellationToken);

        var carModels = new List<CarModel>();

        foreach (var car in cars)
        {
            var carModel = _mapper.Map<CarModel>(car);

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
            var carModel = _mapper.Map<CarModel>(car);

            carModels.Add(carModel);
        }

        return carModels;
    }
}
