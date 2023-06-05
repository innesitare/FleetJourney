using Mediator;

namespace FleetJourney.Application.Messages.Notifications.CarPool;

public sealed class DeleteCarMessage : INotification
{
    public required Guid Id { get; init; }
}