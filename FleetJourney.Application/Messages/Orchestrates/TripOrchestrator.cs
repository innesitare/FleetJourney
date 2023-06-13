using FleetJourney.Application.Mapping;
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
        var message = context.Message;
        _logger.LogInformation("Creating trip for employee with Id: {EmployeeId}", message.EmployeeId);

        var trip = message.ToTrip();
        await _tripService.CreateAsync(trip, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<UpdateTrip> context)
    {
        var message = context.Message;
        _logger.LogInformation("Updating trip with id: {Id}", message.Trip.Id);
        
        await _tripService.UpdateAsync(message.Trip, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DeleteTrip> context)
    {
        var message = context.Message;
        _logger.LogInformation("Deleting trip with id: {Id}", message.Id);
        
        await _tripService.DeleteAsync(message.Id, context.CancellationToken);
    }
}