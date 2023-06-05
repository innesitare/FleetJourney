using Mediator;

namespace FleetJourney.Application.Messages.Commands.Trips;

public sealed class DeleteTripCommand : ICommand<bool>
{
    public required Guid Id { get; init; }
}