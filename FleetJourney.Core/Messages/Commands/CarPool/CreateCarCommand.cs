using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Core.Messages.Commands.CarPool;

public sealed class CreateCarCommand : ICommand<bool>
{
    public required Car Car { get; init; }
}