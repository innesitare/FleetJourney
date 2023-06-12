using FleetJourney.Application.Extensions;
using FleetJourney.Application.Helpers;
using FleetJourney.Application.Services.Abstractions;
using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Application.Messages.Notifications.CarPool.Handlers;

public sealed class CachingCarNotificationHandler : 
    INotificationHandler<CreateCarMessage>,
    INotificationHandler<UpdateCarMessage>, 
    INotificationHandler<DeleteCarMessage>
{
    private readonly ICacheService<Car> _cacheService;

    public CachingCarNotificationHandler(ICacheService<Car> cacheService)
    {
        _cacheService = cacheService;
    }

    public async ValueTask Handle(CreateCarMessage notification, CancellationToken cancellationToken)
    {
        var car = notification.Car;
        
        await _cacheService.RemoveCachesAsync(cancellationToken, 
            CacheKeys.CarPool.GetAll, 
            CacheKeys.CarPool.Get(car.Id)
        );
    }

    public async ValueTask Handle(UpdateCarMessage notification, CancellationToken cancellationToken)
    {
        var car = notification.Car;
        
        await _cacheService.RemoveCachesAsync(cancellationToken, 
            CacheKeys.CarPool.GetAll, 
            CacheKeys.CarPool.Get(car.Id)
        );
    }

    public async ValueTask Handle(DeleteCarMessage notification, CancellationToken cancellationToken)
    {
        var carId = notification.Id;
        
        await _cacheService.RemoveCachesAsync(cancellationToken,
            CacheKeys.CarPool.GetAll,
            CacheKeys.CarPool.Get(carId),
            CacheKeys.Trips.GetAll
        );
    }
}