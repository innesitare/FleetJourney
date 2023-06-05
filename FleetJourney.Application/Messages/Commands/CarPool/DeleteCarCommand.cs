using Mediator;

namespace FleetJourney.Application.Messages.Commands.CarPool;

public sealed class DeleteCarCommand : ICommand<bool>
{
    public required Guid Id { get; init; }
}