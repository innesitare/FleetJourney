using FleetJourney.Application.Mapping;
using FleetJourney.Application.Services.Abstractions;
using FleetJourney.Domain.EmployeeInfo;
using FleetJourney.Domain.Messages.Employees;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FleetJourney.Application.Messages.Orchestrates;

public sealed class EmployeeOrchestrator : 
    IConsumer<CreateEmployee>, 
    IConsumer<UpdateEmployee>, 
    IConsumer<DeleteEmployee>
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeeOrchestrator> _logger;
        
    public EmployeeOrchestrator(IEmployeeService employeeService, ILogger<EmployeeOrchestrator> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateEmployee> context)
    {
        var message = context.Message;
        _logger.LogInformation("Creating employee with email: {Email}", message.Email);
        
        var employee = message.ToEmployee();
        await _employeeService.CreateAsync(employee, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<UpdateEmployee> context)
    {
        var message = context.Message;
        _logger.LogInformation("Updating employee with email: {Email}", message.Employee.Email);
        
        await _employeeService.UpdateAsync(message.Employee, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DeleteEmployee> context)
    {      
        var message = context.Message;
        _logger.LogInformation("Deleting employee with id: {Id}", message.Id);
        
        await _employeeService.DeleteAsync(message.Id, context.CancellationToken);
    }
}