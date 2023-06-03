using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Core.Messages.Queries.Employees;

public sealed class GetEmployeeByIdQuery : IQuery<Employee?>
{
    public required Guid Id { get; init; }
}