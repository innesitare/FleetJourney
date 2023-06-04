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
        _logger.LogInformation("Creating employee with email: {@Email}", context.Message.Email);
        await _employeeService.CreateAsync(new Employee
        {
            Email = context.Message.Email,
            Name = context.Message.Name,
            LastName = context.Message.LastName,
            Birthdate = context.Message.Birthdate
        }, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<UpdateEmployee> context)
    {
        _logger.LogInformation("Updating employee with email: {Email}", context.Message.Employee.Email);
        await _employeeService.UpdateAsync(context.Message.Employee, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DeleteEmployee> context)
    {      
        _logger.LogInformation("Deleting employee with id: {@Id}", context.Message.Id);
        await _employeeService.DeleteAsync(context.Message.Id, context.CancellationToken);
    }
}