using FleetJourney.Application.Mapping;
using FleetJourney.Application.Services.Abstractions;
using FleetJourney.Domain.CarPool;
using FleetJourney.Domain.Messages.CarPool;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FleetJourney.Application.Messages.Orchestrates;

public sealed class CarPoolOrchestrator :
    IConsumer<CreateCar>,
    IConsumer<UpdateCar>,
    IConsumer<DeleteCar>
{
    private readonly ICarPoolService _carPoolService;
    private readonly ILogger<CarPoolOrchestrator> _logger;

    public CarPoolOrchestrator(ICarPoolService carPoolService, ILogger<CarPoolOrchestrator> logger)
    {
        _carPoolService = carPoolService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateCar> context)
    {
        var message = context.Message;
        _logger.LogInformation("Creating car with number: {Id}", message.LicensePlateNumber);
        
        var car = message.ToCar();
        await _carPoolService.CreateAsync(car, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<UpdateCar> context)
    {
        var message = context.Message;
        _logger.LogInformation("Updating car with id: {Id}", message.Car.Id);
        
        await _carPoolService.UpdateAsync(message.Car, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DeleteCar> context)
    {
        var message = context.Message;
        _logger.LogInformation("Deleting car with id: {Id}", message.Id);
        
        await _carPoolService.DeleteAsync(message.Id, context.CancellationToken);
    }
}