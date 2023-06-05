using FleetJourney.Application.Helpers;
using FleetJourney.Application.Messages.Commands.Trips;
using FleetJourney.Application.Messages.Notifications.Trips;
using FleetJourney.Application.Messages.Queries.Employees;
using FleetJourney.Application.Messages.Queries.Trips;
using FleetJourney.Application.Services.Abstractions;
using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Services;

internal sealed class TripService : ITripService
{
    private readonly ISender _sender;
    private readonly ICacheService<Trip> _cacheService;

    public TripService(ISender sender, ICacheService<Trip> cacheService)
    {
        _sender = sender;
        _cacheService = cacheService;
    }

    public Task<IEnumerable<Trip>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _cacheService.GetAllOrCreateAsync(CacheKeys.Trips.GetAll, async () =>
        {
            var trips = await _sender.Send(new GetAllTripsQuery(), cancellationToken);

            return trips;
        }, cancellationToken);
    }

    public Task<IEnumerable<Trip>> GetAllByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        return _cacheService.GetAllOrCreateAsync(CacheKeys.Trips.GetAllByEmployeeId(employeeId), async () =>
        {
            var trips = await _sender.Send(new GetAllTripsByEmployeeIdQuery
            {
                EmployeeId = employeeId
            }, cancellationToken);

            return trips;
        }, cancellationToken);
    }

    public Task<Trip?> GetAsync(Guid tripId, CancellationToken cancellationToken)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.Trips.Get(tripId), async () =>
        {
            var trip = await _sender.Send(new GetTripByIdQuery
            {
                Id = tripId
            }, cancellationToken);

            return trip;
        }, cancellationToken);
    }

    public Task<Trip?> GetTripByCarIdAsync(Guid carId, CancellationToken cancellationToken)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.Trips.GetByCarId(carId), async () =>
        {
            var trip = await _sender.Send(new GetTripByCarIdQuery
            {
                CarId = carId
            }, cancellationToken);

            return trip;
        }, cancellationToken);
    }

    public async Task<bool> CreateAsync(Trip trip, CancellationToken cancellationToken)
    {
        bool isCreated = await _sender.Send(new CreateTripCommand
        {
            Trip = trip
        }, cancellationToken);

        return isCreated;
    }

    public async Task<Trip?> UpdateAsync(Trip trip, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new UpdateTripCommand
        {
            Trip = trip
        }, cancellationToken);

        return result;
    }

    public async Task<bool> DeleteAsync(Guid tripId, CancellationToken cancellationToken)
    {
        bool isDeleted = await _sender.Send(new DeleteTripCommand
        {
            Id = tripId
        }, cancellationToken);

        return isDeleted;
    }
}