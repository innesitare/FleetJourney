using FleetJourney.Application.Messages.Notifications.Employees;
using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.EmployeeInfo;
using FleetJourney.Domain.Messages.Employees;
using MassTransit;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.Employees.Handlers;

public sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, Employee?>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;
    private readonly IPublisher _publisher;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository,
        ISendEndpointProvider sendEndpointProvider, IPublisher publisher)
    {
        _employeeRepository = employeeRepository;
        _sendEndpointProvider = sendEndpointProvider;
        _publisher = publisher;
    }

    public async ValueTask<Employee?> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var updatedEmployee = await _employeeRepository.UpdateAsync(command.Employee, cancellationToken);
        if (updatedEmployee is not null)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:update-employee"));
            await sendEndpoint.Send<UpdateEmployee>(new
            {
                Employee = updatedEmployee
            }, cancellationToken);

            await _publisher.Publish(new UpdateEmployeeMessage
            {
                Employee = updatedEmployee
            }, cancellationToken);
        }

        return updatedEmployee;
    }
}