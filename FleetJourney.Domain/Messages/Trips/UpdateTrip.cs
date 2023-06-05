using FleetJourney.Domain.Trips;

namespace FleetJourney.Domain.Messages.Trips;

public sealed class UpdateTrip
{
    public required Trip Trip { get; init; }
}