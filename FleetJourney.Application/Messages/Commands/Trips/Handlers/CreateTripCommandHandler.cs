using FleetJourney.Application.Repositories.Abstractions;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Trips.Handlers;

public sealed class CreateTripCommandHandler : ICommandHandler<CreateTripCommand, bool>
{
    private readonly ITripRepository _tripRepository;

    public CreateTripCommandHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async ValueTask<bool> Handle(CreateTripCommand command, CancellationToken cancellationToken)
    {
        bool created = await _tripRepository.CreateAsync(command.Trip, cancellationToken);

        return created;
    }
}