﻿using FleetJourney.Core.Helpers;
using FleetJourney.Core.Messages.Commands.CarPool;
using FleetJourney.Core.Messages.Queries.Cars;
using FleetJourney.Core.Services.Abstractions;
using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Core.Services;

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

    public Task<Car?> GetAsync(string licensePlateNumber, CancellationToken cancellationToken)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.CarPool.Get(licensePlateNumber), async () =>
        {
            var car = await _sender.Send(new GetCarByNumberQuery
            {
                LicensePlateNumber = licensePlateNumber
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

        if (isCreated)
        {
            await _cacheService.RemoveAsync(CacheKeys.CarPool.GetAll, cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.CarPool.Get(car.LicensePlateNumber), cancellationToken);
        }

        return isCreated;
    }

    public async Task<Car?> UpdateAsync(Car car, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new UpdateCarCommand
        {
            Car = car
        }, cancellationToken);

        if (result is not null)
        {
            await _cacheService.RemoveAsync(CacheKeys.CarPool.GetAll, cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.CarPool.Get(car.LicensePlateNumber), cancellationToken);
        }

        return result;
    }

    public async Task<bool> DeleteAsync(string licensePlateNumber, CancellationToken cancellationToken)
    {
        bool isDeleted = await _sender.Send(new DeleteCarCommand
        {
            LicensePlateNumber = licensePlateNumber
        }, cancellationToken);

        if (isDeleted)
        {
            await _cacheService.RemoveAsync(CacheKeys.CarPool.GetAll, cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.CarPool.Get(licensePlateNumber), cancellationToken);
        }

        return isDeleted;
    }
}