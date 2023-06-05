using Mediator;

namespace FleetJourney.Application.Messages.Notifications.Trips;

public sealed class DeleteTripMessage : INotification
{
    public required Guid Id { get; init; }
}