using FleetJourney.Core.Messages.Commands.Employees;
using FleetJourney.Domain.EmployeeInfo;
using FleetJourney.Domain.Messages.Employees;
using FleetJourney.Infrastructure.Repositories.Abstractions;
using MassTransit;
using Mediator;

namespace FleetJourney.Core.Messages.Handlers.Employees;

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
        var updateEmployee = await _employeeRepository.UpdateAsync(command.Employee, cancellationToken);
        if (updateEmployee is not null)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:update-employee"));
            await sendEndpoint.Send<UpdateEmployee>(new
            {
                Employee = updateEmployee
            }, cancellationToken);
        }

        return updateEmployee;
    }
}