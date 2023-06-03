using FleetJourney.Infrastructure.Repositories.Abstractions;
using Mediator;

namespace FleetJourney.Core.Messages.Commands.Employees.Handlers;

public sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async ValueTask<bool> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        bool created = await _employeeRepository.CreateAsync(command.Employee, cancellationToken);

        return created;
    }
}