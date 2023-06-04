using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Employees;

public sealed class GetAllEmployeesQuery : IQuery<IEnumerable<Employee>>
{
}