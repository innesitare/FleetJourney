using FleetJourney.Application.Messages.Notifications.Trips;
using FleetJourney.Application.Repositories.Abstractions;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Trips.Handlers;

public sealed class CreateTripCommandHandler : ICommandHandler<CreateTripCommand, bool>
{
    private readonly ITripRepository _tripRepository;
    private readonly IPublisher _publisher;

    public CreateTripCommandHandler(ITripRepository tripRepository, IPublisher publisher)
    {
        _tripRepository = tripRepository;
        _publisher = publisher;
    }

    public async ValueTask<bool> Handle(CreateTripCommand command, CancellationToken cancellationToken)
    {
        bool created = await _tripRepository.CreateAsync(command.Trip, cancellationToken);
        if (created)
        {
            await _publisher.Publish(new CreateTripMessage
            {
                Trip = command.Trip
            }, cancellationToken);
        }

        return created;
    }
}