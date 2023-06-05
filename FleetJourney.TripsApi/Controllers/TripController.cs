using FleetJourney.Application.Contracts.Requests.Trips;
using FleetJourney.Application.Helpers;
using FleetJourney.Application.Mapping;
using FleetJourney.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FleetJourney.TripsApi.Controllers;

[ApiController]
public sealed class TripController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet(ApiEndpoints.Trips.GetAll)]
    public async Task<IActionResult> GetAllTrips(CancellationToken cancellationToken)
    {
        var trips = await _tripService.GetAllAsync(cancellationToken);
        var responses = trips.Select(t => t.ToResponse());

        return Ok(responses);
    }

    [HttpGet(ApiEndpoints.Trips.GetAllByEmployeeId)]
    public async Task<IActionResult> GetAllByEmployeeId([FromRoute] Guid employeeId,
        CancellationToken cancellationToken)
    {
        var trips = await _tripService.GetAllByEmployeeIdAsync(employeeId, cancellationToken);
        var responses = trips.Select(t => t.ToResponse());

        return responses.Any()
            ? Ok(responses)
            : NotFound();
    }

    [HttpGet(ApiEndpoints.Trips.Get)]
    public async Task<IActionResult> GetTrip([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var trip = await _tripService.GetAsync(id, cancellationToken);

        return trip is not null
            ? Ok(trip.ToResponse())
            : NotFound();
    }

    [HttpGet(ApiEndpoints.Trips.GetByCarId)]
    public async Task<IActionResult> GetTripByCarId([FromRoute] Guid carId,
        CancellationToken cancellationToken)
    {
        var trip = await _tripService.GetTripByCarIdAsync(carId, cancellationToken);

        return trip is not null
            ? Ok(trip.ToResponse())
            : NotFound();
    }

    [HttpPost(ApiEndpoints.Trips.Create)]
    public async Task<IActionResult> CreateTrip([FromBody] CreateTripRequest request,
        CancellationToken cancellationToken)
    {
        var trip = request.ToTrip();
        bool isCreated = await _tripService.CreateAsync(trip, cancellationToken);

        return isCreated
            ? CreatedAtAction(nameof(GetTrip), new {id = trip.Id}, trip.ToResponse())
            : BadRequest();
    }

    [HttpPut(ApiEndpoints.Trips.Update)]
    public async Task<IActionResult> UpdateTrip([FromRoute] Guid id, [FromBody] UpdateTripRequest request,
        CancellationToken cancellationToken)
    {
        var trip = request.ToTrip(id);
        var updatedTrip = await _tripService.UpdateAsync(trip, cancellationToken);

        return updatedTrip is not null
            ? Ok(updatedTrip.ToResponse())
            : NotFound();
    }

    [HttpDelete(ApiEndpoints.Trips.Delete)]
    public async Task<IActionResult> DeleteTrip([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _tripService.DeleteAsync(id, cancellationToken);

        return isDeleted
            ? Ok()
            : NotFound();
    }
}