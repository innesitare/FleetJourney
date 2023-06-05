using FleetJourney.Application.Extensions;
using FleetJourney.Application.Helpers;
using FleetJourney.Application.Services.Abstractions;
using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Notifications.Trips.Handlers;

public sealed class CachingTripNotificationHandler : 
    INotificationHandler<CreateTripMessage>,
    INotificationHandler<UpdateTripMessage>, 
    INotificationHandler<DeleteTripMessage>
{
    private readonly ICacheService<Trip> _cacheService;

    public CachingTripNotificationHandler(ICacheService<Trip> cacheService)
    {
        _cacheService = cacheService;
    }

    public async ValueTask Handle(CreateTripMessage notification, CancellationToken cancellationToken)
    {
        var trip = notification.Trip;
        
        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.Trips.GetAll,
            CacheKeys.Trips.Get(trip.Id),
            CacheKeys.Trips.GetAllByEmployeeId(trip.EmployeeId),
            CacheKeys.Trips.GetByCarId(trip.CarId),
            CacheKeys.CarPool.GetAll,
            CacheKeys.CarPool.Get(trip.CarId),
            CacheKeys.Employees.GetAll,
            CacheKeys.Employees.Get(trip.EmployeeId)
        );
    }
    
    public async ValueTask Handle(UpdateTripMessage notification, CancellationToken cancellationToken)
    {
        var trip = notification.Trip;

        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.Trips.GetAll,
            CacheKeys.Trips.Get(trip.Id),
            CacheKeys.Trips.GetAllByEmployeeId(trip.EmployeeId),
            CacheKeys.Trips.GetByCarId(trip.CarId),
            CacheKeys.CarPool.GetAll,
            CacheKeys.CarPool.Get(trip.CarId),
            CacheKeys.Employees.GetAll,
            CacheKeys.Employees.Get(trip.EmployeeId)
        );
    }
    
    public async ValueTask Handle(DeleteTripMessage notification, CancellationToken cancellationToken)
    {
        var tripId = notification.Id;
        
        await _cacheService.RemoveCachesAsync(cancellationToken, 
            CacheKeys.Trips.GetAll,
            CacheKeys.Trips.Get(tripId)
        );
    }
}