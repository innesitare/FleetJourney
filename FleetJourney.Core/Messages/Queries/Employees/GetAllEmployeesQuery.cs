using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Core.Messages.Queries.Employees;

public sealed class GetAllEmployeesQuery : IQuery<IEnumerable<Employee>>
{
}