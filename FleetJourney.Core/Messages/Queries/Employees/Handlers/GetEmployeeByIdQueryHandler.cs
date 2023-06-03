using FleetJourney.Domain.EmployeeInfo;
using FleetJourney.Infrastructure.Repositories.Abstractions;
using Mediator;

namespace FleetJourney.Core.Messages.Queries.Employees.Handlers;

public sealed class GetEmployeeByIdQueryHandler : IQueryHandler<GetEmployeeByIdQuery, Employee?>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async ValueTask<Employee?> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetAsync(query.Id, cancellationToken);

        return employee;
    }
}