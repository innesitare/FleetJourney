using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Core.Messages.Queries.Employees;

public sealed class GetEmployeeByEmailQuery : IQuery<Employee?>
{
    public required string Email { get; init; }
}