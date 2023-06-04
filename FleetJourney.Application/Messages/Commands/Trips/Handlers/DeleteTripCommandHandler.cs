using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Messages.Trips;
using MassTransit;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Trips.Handlers;

public sealed class DeleteTripCommandHandler : ICommandHandler<DeleteTripCommand, bool>
{
    private readonly ITripRepository _tripRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public DeleteTripCommandHandler(ITripRepository tripRepository, ISendEndpointProvider sendEndpointProvider)
    {
        _tripRepository = tripRepository;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async ValueTask<bool> Handle(DeleteTripCommand command, CancellationToken cancellationToken)
    {
        var trip = await _tripRepository.GetAsync(command.Id, cancellationToken);
        if (trip is not null)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:delete-trip"));
            await sendEndpoint.Send<DeleteTrip>(new
            {
                trip.Id
            }, cancellationToken);
        }
        
        bool deleted = await _tripRepository.DeleteAsync(command.Id, cancellationToken);
        return deleted;
    }
}