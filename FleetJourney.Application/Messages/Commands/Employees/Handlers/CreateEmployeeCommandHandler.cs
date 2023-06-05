using FleetJourney.Application.Messages.Notifications.Employees;
using FleetJourney.Application.Repositories.Abstractions;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Employees.Handlers;

public sealed class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPublisher _publisher;

    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IPublisher publisher)
    {
        _employeeRepository = employeeRepository;
        _publisher = publisher;
    }

    public async ValueTask<bool> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        bool created = await _employeeRepository.CreateAsync(command.Employee, cancellationToken);
        if (created)
        {
            await _publisher.Publish(new CreateEmployeeMessage
            {
                Employee = command.Employee
            }, cancellationToken);
        }
        
        return created;
    }
}