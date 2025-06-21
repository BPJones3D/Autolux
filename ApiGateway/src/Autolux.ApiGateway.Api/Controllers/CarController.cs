using Autolux.CoreApp.Api.Entities;
using Autolux.CoreApp.Models.Cars;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Autolux.ApiGateway.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly ICarService _carService;

    public CarController(ICarService carService)
    {
        _carService = carService;
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
}
