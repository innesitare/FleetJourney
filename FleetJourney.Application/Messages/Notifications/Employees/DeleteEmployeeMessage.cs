using Mediator;

namespace FleetJourney.Application.Messages.Notifications.Employees;

public sealed class DeleteEmployeeMessage : INotification
{
    public required Guid Id { get; init; }
}