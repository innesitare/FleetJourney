using FleetJourney.Application.Contracts.Requests.CarPool;
using FleetJourney.Application.Helpers;
using FleetJourney.Application.Mapping;
using FleetJourney.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FleetJourney.CarPoolApi.Controllers;

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
    public async Task<IActionResult> GetCar([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var car = await _carPoolService.GetAsync(id, cancellationToken);

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
            ? CreatedAtAction(nameof(GetCar), new {id = car.Id}, car.ToResponse())
            : BadRequest();
    }

    [HttpPut(ApiEndpoints.CarPool.Update)]
    public async Task<IActionResult> UpdateCar([FromRoute] Guid id,
        [FromBody] UpdateCarRequest request, CancellationToken cancellationToken)
    {
        var car = request.ToCar(id);
        var updatedCar = await _carPoolService.UpdateAsync(car, cancellationToken);

        return updatedCar is not null
            ? Ok(updatedCar.ToResponse())
            : NotFound();
    }

    [HttpDelete(ApiEndpoints.CarPool.Delete)]
    public async Task<IActionResult> DeleteCar([FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        bool isDeleted = await _carPoolService.DeleteAsync(id, cancellationToken);

        return isDeleted
            ? Ok()
            : NotFound();
    }
}