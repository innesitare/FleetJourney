using FleetJourney.Application.Messages.Notifications.Employees;
using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Messages.Employees;
using MassTransit;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Employees.Handlers;

public sealed class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IPublisher _publisher;

    public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository,
        ISendEndpointProvider sendEndpointProvider, IPublisher publisher)
    {
        _employeeRepository = employeeRepository;
        _sendEndpointProvider = sendEndpointProvider;
        _publisher = publisher;
    }

    public async ValueTask<bool> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetAsync(command.Id, cancellationToken);
        if (employee is not null)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:delete-employee"));
            await sendEndpoint.Send<DeleteEmployee>(new
            {
                Id = employee.Id
            }, cancellationToken);

            await _publisher.Publish(new DeleteEmployeeMessage
            {
                Id = employee.Id
            }, cancellationToken);
        }

        bool deleted = await _employeeRepository.DeleteAsync(command.Id, cancellationToken);
        return deleted;
    }
}