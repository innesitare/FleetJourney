namespace FleetJourney.Domain.Messages.Trips;

public sealed class DeleteTrip
{
    public required Guid Id { get; init; }
}