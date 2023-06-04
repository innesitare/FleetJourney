using FleetJourney.Application.Helpers;
using FleetJourney.Application.Messages.Commands.Trips;
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

    public Task<Trip?> GetByCarPlateNumberAsync(string licensePlateNumber, CancellationToken cancellationToken)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.Trips.GetByLicensePlateNumber(licensePlateNumber), async () =>
        {
            var trip = await _sender.Send(new GetTripByCarPlateNumberQuery
            {
                LicensePlateNumber = licensePlateNumber
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

        if (isCreated)
        {
            await _cacheService.RemoveAsync(CacheKeys.Trips.GetAll, cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Trips.Get(trip.Id), cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Trips.GetAllByEmployeeId(trip.EmployeeId), cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Trips.GetByLicensePlateNumber(trip.LicensePlateNumber), cancellationToken);
        }

        return isCreated;
    }

    public async Task<Trip?> UpdateAsync(Trip trip, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new UpdateTripCommand
        {
            Trip = trip
        }, cancellationToken);

        if (result is not null)
        {
            await _cacheService.RemoveAsync(CacheKeys.Trips.GetAll, cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Trips.Get(trip.Id), cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Trips.GetAllByEmployeeId(trip.EmployeeId), cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Trips.GetByLicensePlateNumber(trip.LicensePlateNumber), cancellationToken);
        }

        return result;
    }

    public async Task<bool> DeleteAsync(Guid tripId, CancellationToken cancellationToken)
    {
        bool isDeleted = await _sender.Send(new DeleteTripCommand
        {
            Id = tripId
        }, cancellationToken);

        if (isDeleted)
        {
            await _cacheService.RemoveAsync(CacheKeys.Trips.GetAll, cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Trips.Get(tripId), cancellationToken);
        }

        return isDeleted;
    }
}