using FleetJourney.Application.Contracts.Requests.CarPool;
using FleetJourney.Application.Helpers;
using FleetJourney.Application.Mapping;
using FleetJourney.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FleetJourney.CarPoolApi.Controllers;

// [Authorize]
[ApiController]
public sealed class CarPoolController : ControllerBase
{
    private readonly ICarPoolService _carPoolService;

    public CarPoolController(ICarPoolService carPoolService)
    {
        _carPoolService = carPoolService;
    }

    [HttpGet(ApiEndpoints.CarPool.GetAll)]
    public async Task<IActionResult> GetAllCars(CancellationToken cancellationToken)
    {
        var cars = await _carPoolService.GetAllAsync(cancellationToken);
        var responses = cars.Select(c => c.ToResponse());

        return Ok(responses);
    }

    [HttpGet(ApiEndpoints.CarPool.Get)]
    public async Task<IActionResult> GetCar([FromRoute] string licensePlateNumber, CancellationToken cancellationToken)
    {
        var car = await _carPoolService.GetAsync(licensePlateNumber, cancellationToken);

        return car is not null
            ? Ok(car.ToResponse())
            : NotFound();
    }

    [HttpPost(ApiEndpoints.CarPool.Create)]
    public async Task<IActionResult> CreateCar([FromBody] CreateCarRequest request, CancellationToken cancellationToken)
    {
        var car = request.ToCar();
        bool isCreated = await _carPoolService.CreateAsync(car, cancellationToken);

        return isCreated
            ? CreatedAtAction(nameof(GetCar), new {licensePlateNumber = car.LicensePlateNumber},
                car.ToResponse())
            : BadRequest();
    }

    [HttpPut(ApiEndpoints.CarPool.Update)]
    public async Task<IActionResult> UpdateCar([FromRoute] string licensePlateNumber,
        [FromBody] UpdateCarRequest request, CancellationToken cancellationToken)
    {
        var car = request.ToCar(licensePlateNumber);
        var updatedCar = await _carPoolService.UpdateAsync(car, cancellationToken);

        return updatedCar is not null
            ? Ok(updatedCar.ToResponse())
            : NotFound();
    }

    [HttpDelete(ApiEndpoints.CarPool.Delete)]
    public async Task<IActionResult> DeleteCar([FromRoute] string licensePlateNumber,
        CancellationToken cancellationToken)
    {
        bool isDeleted = await _carPoolService.DeleteAsync(licensePlateNumber, cancellationToken);

        return isDeleted
            ? Ok()
            : NotFound();
    }
}