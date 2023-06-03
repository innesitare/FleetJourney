using FleetJourney.Domain.Messages.Employees;
using FleetJourney.Infrastructure.Repositories.Abstractions;
using MassTransit;
using Mediator;

namespace FleetJourney.Core.Messages.Commands.Employees.Handlers;

public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ISendEndpointProvider _endpointProvider;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, ISendEndpointProvider endpointProvider)
    {
        _employeeRepository = employeeRepository;
        _endpointProvider = endpointProvider;
    }

    public async ValueTask<bool> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetAsync(command.Id, cancellationToken);
        if (employee is not null)
        {
            var sendEndpoint = await _endpointProvider.GetSendEndpoint(new Uri("queue:delete-employee"));
            await sendEndpoint.Send<DeleteEmployee>(new
            {
                employee.Email
            }, cancellationToken);
        }
        
        bool deleted = await _employeeRepository.DeleteAsync(command.Id, cancellationToken);
        return deleted;
    }
}