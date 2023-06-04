using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.CarPool;

public sealed class UpdateCarCommand : ICommand<Car?>
{
    public required Car Car { get; init; }
}