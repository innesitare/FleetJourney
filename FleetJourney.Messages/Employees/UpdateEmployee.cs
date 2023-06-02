using FleetJourney.Domain.EmployeeInfo;

namespace FleetJourney.Messages.Employees;

public sealed class UpdateEmployee
{
    public required Employee Employee { get; init; }
}