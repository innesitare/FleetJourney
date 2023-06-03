using FleetJourney.Domain.EmployeeInfo;
using FleetJourney.Infrastructure.Repositories.Abstractions;
using Mediator;

namespace FleetJourney.Core.Messages.Queries.Employees.Handlers;

public sealed class GetEmployeeByEmailQueryHandler : IQueryHandler<GetEmployeeByEmailQuery, Employee?>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByEmailQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async ValueTask<Employee?> Handle(GetEmployeeByEmailQuery query, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByEmailAsync(query.Email, cancellationToken);

        return employee;
    }
}