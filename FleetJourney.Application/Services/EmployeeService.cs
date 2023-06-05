using FleetJourney.Application.Helpers;
using FleetJourney.Application.Messages.Commands.Employees;
using FleetJourney.Application.Messages.Queries.Employees;
using FleetJourney.Application.Services.Abstractions;
using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Application.Services;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly ISender _sender;
    private readonly ICacheService<Employee> _cacheService;

    public EmployeeService(ISender sender, ICacheService<Employee> cacheService)
    {
        _sender = sender;
        _cacheService = cacheService;
    }

    public Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _cacheService.GetAllOrCreateAsync(CacheKeys.Employees.GetAll, async () =>
        {
            var employees = await _sender.Send(new GetAllEmployeesQuery(), cancellationToken);

            return employees;
        }, cancellationToken);
    }

    public Task<Employee?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.Employees.Get(id), async () =>
        {
            var employee = await _sender.Send(new GetEmployeeByIdQuery
            {
                Id = id
            }, cancellationToken);

            return employee;
        }, cancellationToken);
    }

    public Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _cacheService.GetOrCreateAsync(CacheKeys.Employees.GetByEmail(email), async () =>
        {
            var employee = await _sender.Send(new GetEmployeeByEmailQuery()
            {
                Email = email
            }, cancellationToken);

            return employee;
        }, cancellationToken);
    }

    public async Task<bool> CreateAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        bool isCreated = await _sender.Send(new CreateEmployeeCommand
        {
            Employee = employee
        }, cancellationToken);
        
        return isCreated;
    }

    public async Task<Employee?> UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new UpdateEmployeeCommand
        {
            Employee = employee
        }, cancellationToken);
        
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _sender.Send(new DeleteEmployeeCommand
        {
            Id = id
        }, cancellationToken);
        
        return isDeleted;
    }
}