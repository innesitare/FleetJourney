using FleetJourney.Application.Messages.Notifications.Trips;
using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Messages.Trips;
using FleetJourney.Domain.Trips;
using MassTransit;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Trips.Handlers;

public sealed class UpdateTripCommandHandler : ICommandHandler<UpdateTripCommand, Trip?>
{
    private readonly ITripRepository _tripRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IPublisher _publisher;

    public UpdateTripCommandHandler(ITripRepository tripRepository, ISendEndpointProvider sendEndpointProvider,
        IPublisher publisher)
    {
        _tripRepository = tripRepository;
        _sendEndpointProvider = sendEndpointProvider;
        _publisher = publisher;
    }

    public async ValueTask<Trip?> Handle(UpdateTripCommand command, CancellationToken cancellationToken)
    {
        var updatedTrip = await _tripRepository.UpdateAsync(command.Trip, cancellationToken);
        if (updatedTrip is not null)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:update-trip"));
            await sendEndpoint.Send<UpdateTrip>(new
            {
                Trip = updatedTrip
            }, cancellationToken);
            
            await _publisher.Publish(new UpdateTripMessage
            {
                Trip = command.Trip
            }, cancellationToken);
        }

        return updatedTrip;
    }
}