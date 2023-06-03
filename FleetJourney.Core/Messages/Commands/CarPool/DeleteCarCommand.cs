using Mediator;

namespace FleetJourney.Core.Messages.Commands.CarPool;

public sealed class DeleteCarCommand : ICommand<bool>
{
    public required string LicensePlateNumber { get; init; }
}