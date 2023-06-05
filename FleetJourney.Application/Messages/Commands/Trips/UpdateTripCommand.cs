using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Trips;

public sealed class UpdateTripCommand : ICommand<Trip?>
{
    public required Trip Trip { get; init; }
}