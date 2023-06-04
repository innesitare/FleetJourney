using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Messages.Employees;
using MassTransit;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Employees.Handlers;

public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, ISendEndpointProvider sendEndpointProvider)
    {
        _employeeRepository = employeeRepository;
        _sendEndpointProvider = sendEndpointProvider;
    }

    public async ValueTask<bool> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetAsync(command.Id, cancellationToken);
        if (employee is not null)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:delete-employee"));
            await sendEndpoint.Send<DeleteEmployee>(new
            {
                employee.Email
            }, cancellationToken);
        }
        
        bool deleted = await _employeeRepository.DeleteAsync(command.Id, cancellationToken);
        return deleted;
    }
}