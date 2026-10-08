using Autolux.CoreApp.Api.Entities;
using Autolux.CoreApp.Models.Cars;
using Autolux.CoreApp.Models.Shared;
using Autolux.SharedKernel.SharedObjects;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using Swashbuckle.AspNetCore.Annotations;
using System.Buffers;

namespace Autolux.ApiGateway.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    public CarController(ICarService carService, IMapper mapper)
    {
        _carService = carService;
        _mapper = mapper;
    }

    [HttpPost]
    [Authorize(Policy = nameof(PermissionKey.VehicleWrite))]
    [SwaggerOperation(Summary = "Add a new car", Tags = ["Cars"])]
    public async Task<ActionResult<CarModel>> AddAsync(CarCreateModel carCreateModel, CancellationToken cancellationToken)
    {
        if (carCreateModel == null)
            return BadRequest();

        var addedCar = await _carService.AddAsync(carCreateModel, cancellationToken);

        return Ok(addedCar);
    }

    [HttpDelete]
    [Authorize(Policy = nameof(PermissionKey.VehicleDelete))]
    [SwaggerOperation(Summary = "Delete an existing car", Tags = ["Cars"])]
    public async Task<ActionResult> DeleteAsync(CarDeleteModel carDeleteModel, CancellationToken cancellationToken)
    {
        if (carDeleteModel == null)
            return BadRequest();

        await _carService.DeleteAsync(carDeleteModel, cancellationToken);

        return Ok();
    }

    [HttpPut]
    [Authorize(Policy = nameof(PermissionKey.VehicleUpdate))]
    [SwaggerOperation(Summary = "Update an existing car", Tags = ["Cars"])]
    public async Task<ActionResult<List<CarModel>>> UpdateAsync(CarUpdateModel carUpdateModel, CancellationToken cancellationToken)
    {
        if (carUpdateModel == null)
            return BadRequest();

        var updatedCar = await _carService.UpdateAsync(carUpdateModel, cancellationToken);

        return Ok(updatedCar);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get a list of all cars", Tags = ["Cars"])]
    public async Task<ActionResult<List<CarSummaryModel>>> GetSummaryListAsync(
        CancellationToken cancellationToken,
        [FromQuery] List<string> fuelType,
        [FromQuery] List<string> transmission,
        [FromQuery] List<string> brand,
        int page = 1,
        int pageSize = 4,
        string filter = "Relevancy",
        string filterOrder = "Descending",
        string search_value = "",
        int year_min = 0,
        int year_max = 1000000,
        float price_min = 0,
        float price_max = 1000000,
        int miles_min = 0,
        int miles_max = 1000000,
        int mpg_min = 0,
        int mpg_max = 1000000,
        float tankCapacity_min = 0,
        float tankCapacity_max = 1000000,
        int evRange_min = 0,
        int evRange_max = 1000000,
        int seatCount_min = 0,
        int seatCount_max = 1000000,
        int doorCount_min = 0,
        int doorCount_max = 1000000
        )
    {

        var carFilters = new FiltersModel
        {
            filter = filter,
            filterOrder = filterOrder,
            search_value = search_value,
            year_min = year_min,
            year_max = year_max,
            price_min = price_min,
            price_max = price_max,
            miles_min = miles_min,
            miles_max = miles_max,
            mpg_min = mpg_min,
            mpg_max = mpg_max,
            tankCapacity_min = tankCapacity_min,
            tankCapacity_max = tankCapacity_max,
            evRange_min = evRange_min,
            evRange_max = evRange_max,
            seatCount_min = seatCount_min,
            seatCount_max = seatCount_max,
            doorCount_min = doorCount_min,
            doorCount_max = doorCount_max,
            fuelType = fuelType,
            transmission = transmission,
            brand = brand
        };

        // filtering:
        //      by ID (relevancy)
        //      by Price

        var allCars = await _carService.GetSummaryListAsync(carFilters, cancellationToken);

        // used here: https://www.youtube.com/watch?v=fhomCI3bsIM
        var totalCars = allCars.Count;
        var totalPages = (int)Math.Ceiling((decimal)totalCars/ pageSize);
        var carsPerPage = allCars
            .Skip((page-1) * pageSize)
            .Take(pageSize)
            .ToList();
        // --

        return Ok(carsPerPage);
    }

    [HttpGet("car-quantity")]
    [SwaggerOperation(Summary = "Get quantity of cars", Tags = ["Cars"])]
    public async Task<ActionResult<List<CarSummaryModel>>> GetCarQuantityAsync(CancellationToken cancellationToken)
    {
        var carFilters = new FiltersModel{};

        var allCars = await _carService.GetSummaryListAsync(carFilters, cancellationToken);

        return Ok(allCars.Count);
    }

    [HttpGet("search-by-carId-{id}")]
    [SwaggerOperation(Summary = "Get a car by id", Tags = ["Cars"])]
    public async Task<ActionResult<CarModel>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
            return BadRequest();

        var foundCar = await _carService.GetByIdAsync(id, cancellationToken);

        return Ok(foundCar);
    }

    [HttpGet("search-by-carBrand-{brand}")]
    [SwaggerOperation(Summary = "Get a car by its brand", Tags = ["Cars"])]
    public async Task<ActionResult<CarModel>> GetByBrandAsync(string brand, CancellationToken cancellationToken)
    {
        if (brand == null)
            return BadRequest();

        var foundCars = await _carService.GetListByBrandAsync(brand, cancellationToken);

        return Ok(foundCars);
    }

    [HttpGet("search-by-year-{year}")]
    [SwaggerOperation(Summary = "Get a car by its year", Tags = ["Cars"])]
    public async Task<ActionResult<CarModel>> GetByYearAsync(int year, CancellationToken cancellationToken)
    {
        if (year < 1800)
            return BadRequest();

        var foundCars = await _carService.GetListByYearAsync(year, cancellationToken);

        return Ok(foundCars);
    }

    [HttpGet("search-by-priceBracket-{lowPrice}-{highPrice}")]
    [SwaggerOperation(Summary = "Get a car by price bracket", Tags = ["Cars"])]
    public async Task<ActionResult<CarModel>> GetByPriceBracketAsync(double lowPrice, double highPrice, CancellationToken cancellationToken)
    {
        if (lowPrice < 0.00)
            return BadRequest();

        if (highPrice < lowPrice)
            return BadRequest();

        var foundCars = await _carService.GetListByPriceBracketAsync(lowPrice, highPrice, cancellationToken);

        return Ok(foundCars);
    }

}
