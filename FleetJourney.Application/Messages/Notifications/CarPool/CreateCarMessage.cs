using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Application.Messages.Notifications.CarPool;

public sealed class CreateCarMessage : INotification
{
    public required Car Car { get; init; }
}