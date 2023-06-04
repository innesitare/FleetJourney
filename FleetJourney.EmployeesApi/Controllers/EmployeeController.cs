using FleetJourney.Application.Contracts.Requests.Employees;
using FleetJourney.Application.Helpers;
using FleetJourney.Application.Mapping;
using FleetJourney.Application.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace FleetJourney.EmployeesApi.Controllers;

// [Authorize]
[ApiController]
public sealed class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet(ApiEndpoints.Employees.GetAll)]
    public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
    {
        var employees = await _employeeService.GetAllAsync(cancellationToken);
        var responses = employees.Select(e => e.ToResponse());

        return Ok(responses);
    }

    [HttpGet(ApiEndpoints.Employees.Get)]
    public async Task<IActionResult> GetEmployee([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var employee = await _employeeService.GetAsync(id, cancellationToken);

        return employee is not null
            ? Ok(employee.ToResponse())
            : NotFound();
    }

    [HttpGet(ApiEndpoints.Employees.GetByEmail)]
    public async Task<IActionResult> GetEmployeeByEmail([FromRoute] string email, CancellationToken cancellationToken)
    {
        var employee = await _employeeService.GetByEmailAsync(email, cancellationToken);

        return employee is not null
            ? Ok(employee.ToResponse())
            : NotFound();
    }

    [HttpPost(ApiEndpoints.Employees.Create)]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var employee = request.ToEmployee();
        bool isCreated = await _employeeService.CreateAsync(employee, cancellationToken);

        return isCreated
            ? CreatedAtAction(nameof(GetEmployee), new {id = employee.Id}, employee.ToResponse())
            : BadRequest();
    }

    [HttpPut(ApiEndpoints.Employees.Update)]
    public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, [FromBody] UpdateEmployeeRequest request,
        CancellationToken cancellationToken)
    {
        var employee = request.ToEmployee(id);
        var updatedEmployee = await _employeeService.UpdateAsync(employee, cancellationToken);

        return updatedEmployee is not null
            ? Ok(updatedEmployee.ToResponse())
            : NotFound();
    }

    [HttpDelete(ApiEndpoints.Employees.Delete)]
    public async Task<IActionResult> DeleteEmployeeById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _employeeService.DeleteAsync(id, cancellationToken);

        return isDeleted
            ? Ok()
            : NotFound();
    }
}