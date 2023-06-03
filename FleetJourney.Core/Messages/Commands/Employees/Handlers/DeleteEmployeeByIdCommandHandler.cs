using FleetJourney.Core.Messages.Commands.Employees;
using FleetJourney.Domain.Messages.Employees;
using FleetJourney.Infrastructure.Repositories.Abstractions;
using MassTransit;
using Mediator;

namespace FleetJourney.Core.Messages.Handlers.Employees;

public sealed class DeleteEmployeeByIdCommandHandler : ICommandHandler<DeleteEmployeeByIdCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ISendEndpointProvider _endpointProvider;

    public DeleteEmployeeByIdCommandHandler(IEmployeeRepository employeeRepository, ISendEndpointProvider endpointProvider)
    {
        _employeeRepository = employeeRepository;
        _endpointProvider = endpointProvider;
    }

    public async ValueTask<bool> Handle(DeleteEmployeeByIdCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(command.Id, cancellationToken);
        if (employee is not null)
        {
            var sendEndpoint = await _endpointProvider.GetSendEndpoint(new Uri("queue:delete-employee"));
            await sendEndpoint.Send<DeleteEmployee>(new
            {
                employee.Email
            }, cancellationToken);
        }
        
        bool deleted = await _employeeRepository.DeleteByIdAsync(command.Id, cancellationToken);
        return deleted;
    }
}