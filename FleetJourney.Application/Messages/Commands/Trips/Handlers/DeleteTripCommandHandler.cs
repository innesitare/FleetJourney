using FleetJourney.Application.Messages.Notifications.Trips;
using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Messages.Trips;
using MassTransit;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Trips.Handlers;

public sealed class DeleteTripCommandHandler : ICommandHandler<DeleteTripCommand, bool>
{
    private readonly ITripRepository _tripRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IPublisher _publisher;

    public DeleteTripCommandHandler(ITripRepository tripRepository, ISendEndpointProvider sendEndpointProvider,
        IPublisher publisher)
    {
        _tripRepository = tripRepository;
        _sendEndpointProvider = sendEndpointProvider;
        _publisher = publisher;
    }

    public async ValueTask<bool> Handle(DeleteTripCommand command, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetAsync(command.Id, cancellationToken);
        if (trip is not null)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:delete-trip"));
            await sendEndpoint.Send<DeleteTrip>(new
            {
                Id = trip.Id
            }, cancellationToken);
            
            await _publisher.Publish(new DeleteTripMessage
            {
                Id = trip.Id
            }, cancellationToken);
        }

        bool deleted = await _tripRepository.DeleteAsync(command.Id, cancellationToken);
        return deleted;
    }
}