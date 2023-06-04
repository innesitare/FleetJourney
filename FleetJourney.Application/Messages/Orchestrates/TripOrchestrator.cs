using FleetJourney.Application.Services.Abstractions;
using FleetJourney.Domain.Messages.Trips;
using FleetJourney.Domain.Trips;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FleetJourney.Application.Messages.Orchestrates;

public sealed class TripOrchestrator :
    IConsumer<CreateTrip>,
    IConsumer<UpdateTrip>,
    IConsumer<DeleteTrip>
{
    private readonly ITripService _tripService;
    private readonly ILogger<TripOrchestrator> _logger;

    public TripOrchestrator(ITripService tripService, ILogger<TripOrchestrator> logger)
    {
        _tripService = tripService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateTrip> context)
    {
        _logger.LogInformation("Creating trip for employee with Id: {@EmployeeId}", context.Message.EmployeeId);
        await _tripService.CreateAsync(new Trip
        {
            LicensePlateNumber = context.Message.LicensePlateNumber,
            StartMileage = context.Message.StartMileage,
            EndMileage = context.Message.EndMileage,
            IsPrivateTrip = context.Message.IsPrivateTrip,
            EmployeeId = context.Message.EmployeeId,
        }, context.CancellationToken);
        
    }

    public async Task Consume(ConsumeContext<UpdateTrip> context)
    {
        _logger.LogInformation("Updating trip with id: {@Id}", context.Message.Trip.Id);
        await _tripService.UpdateAsync(context.Message.Trip, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DeleteTrip> context)
    {
        _logger.LogInformation("Deleting trip with id: {@Id}", context.Message.Id);
        await _tripService.DeleteAsync(context.Message.Id, context.CancellationToken);
    }
}