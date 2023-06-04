using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Trips;

public sealed class CreateTripCommand : ICommand<bool>
{
    public required Trip Trip { get; init; }
}