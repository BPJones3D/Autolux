using Autolux.CoreApp.Api.Entities;
using Autolux.CoreApp.Models.Cars;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerOperation(Summary = "Add a new car", Tags = ["Cars"])]
    public async Task<ActionResult<CarModel>> AddAsync(CarCreateModel carCreateModel, CancellationToken cancellationToken)
    {
        if (carCreateModel == null)
            return BadRequest();

        var addedCar = await _carService.AddAsync(carCreateModel, cancellationToken);

        return Ok(addedCar);
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Delete an existing car", Tags = ["Cars"])]
    public async Task<ActionResult> DeleteAsync(CarDeleteModel carDeleteModel, CancellationToken cancellationToken)
    {
        if (carDeleteModel == null)
            return BadRequest();

        await _carService.DeleteAsync(carDeleteModel, cancellationToken);

        return Ok();
    }

    [HttpPut]
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
    public async Task<ActionResult<List<CarSummaryModel>>> GetSummaryListAsync(CancellationToken cancellationToken)
    {
        var allCars = await _carService.GetSummaryListAsync(cancellationToken);

        return Ok(allCars);
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
