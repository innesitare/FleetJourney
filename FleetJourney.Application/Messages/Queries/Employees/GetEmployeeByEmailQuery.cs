using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Employees;

public sealed class GetEmployeeByEmailQuery : IQuery<Employee?>
{
    public required string Email { get; init; }
}