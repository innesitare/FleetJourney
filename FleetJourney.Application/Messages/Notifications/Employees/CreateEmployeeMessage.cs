using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Application.Messages.Notifications.Employees;

public sealed class CreateEmployeeMessage : INotification
{
    public required Employee Employee { get; init; }
}