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
    private readonly ILogger<EmployeeOrchestrator> _logger;

    public CarPoolOrchestrator(ICarPoolService carPoolService, ILogger<EmployeeOrchestrator> logger)
    {
        _carPoolService = carPoolService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateCar> context)
    {
        _logger.LogInformation("Creating car with number: {PlateNumber}", context.Message.LicensePlateNumber);
        await _carPoolService.CreateAsync(new Car
        {
            LicensePlateNumber = context.Message.LicensePlateNumber,
            Brand = context.Message.Brand,
            Model = context.Message.Model,
            EndOfLifeMileage = context.Message.EndOfLifeMileage,
            MaintenanceInterval = context.Message.MaintenanceInterval,
            CurrentMileage = context.Message.CurrentMileage
        }, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<UpdateCar> context)
    {
        _logger.LogInformation("Updating car with number: {PlateNumber}", context.Message.Car.LicensePlateNumber);
        await _carPoolService.UpdateAsync(context.Message.Car, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DeleteCar> context)
    {
        _logger.LogInformation("Deleting car with number: {PlateNumber}", context.Message.LicensePlateNumber);
        await _carPoolService.DeleteAsync(context.Message.LicensePlateNumber, context.CancellationToken);
    }
}