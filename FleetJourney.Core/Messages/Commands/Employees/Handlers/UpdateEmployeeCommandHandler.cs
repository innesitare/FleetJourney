using FleetJourney.Domain.EmployeeInfo;
using FleetJourney.Domain.Messages.Employees;
using FleetJourney.Infrastructure.Repositories.Abstractions;
using MassTransit;
using Mediator;

namespace FleetJourney.Core.Messages.Commands.Employees.Handlers;

public sealed class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, Employee?>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, ISendEndpointProvider sendEndpointProvider)
    {
        _employeeRepository = employeeRepository;
        _sendEndpointProvider = sendEndpointProvider;
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
        }

        return updatedEmployee;
    }
}