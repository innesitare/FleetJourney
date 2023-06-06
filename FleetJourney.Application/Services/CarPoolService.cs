using FleetJourney.Application.Helpers;
using FleetJourney.Application.Messages.Commands.CarPool;
using FleetJourney.Application.Messages.Queries.Cars;
using FleetJourney.Application.Services.Abstractions;
using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Application.Services;

internal sealed class CarPoolService : ICarPoolService
{
    private readonly ISender _sender;
    private readonly ICacheService<Car> _cacheService;

    public CarPoolService(ISender sender, ICacheService<Car> cacheService)
    {
        _sender = sender;
        _cacheService = cacheService;
    }
    
    public Task<IEnumerable<Car>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _cacheService.GetAllOrCreateAsync(CacheKeys.CarPool.GetAll, async () =>
        {
            var cars = await _sender.Send(new GetAllCarsQuery(), cancellationToken);

            return cars;
        }, cancellationToken);
    }

    public Task<Car?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.CarPool.Get(id), async () =>
        {
            var car = await _sender.Send(new GetCarByIdQuery
            {
                Id = id
            }, cancellationToken);

            return car;
        }, cancellationToken);
    }
    
    public async Task<bool> CreateAsync(Car car, CancellationToken cancellationToken)
    {
        bool isCreated = await _sender.Send(new CreateCarCommand
        {
            Car = car
        }, cancellationToken);
        
        return isCreated;
    }

    public async Task<Car?> UpdateAsync(Car car, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new UpdateCarCommand
        {
            Car = car
        }, cancellationToken);

        return result;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _sender.Send(new DeleteCarCommand
        {
            Id = id
        }, cancellationToken);
        
        return isDeleted;
    }
}