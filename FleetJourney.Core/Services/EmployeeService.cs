using FleetJourney.Core.Helpers;
using FleetJourney.Core.Messages.Commands.Employees;
using FleetJourney.Core.Messages.Queries.Employees;
using FleetJourney.Core.Services.Abstractions;
using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Core.Services;

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

    public Task<Employee?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
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

        if (isCreated)
        {
            await _cacheService.RemoveAsync(CacheKeys.Employees.GetAll, cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Employees.Get(employee.Id), cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Employees.GetByEmail(employee.Email), cancellationToken);
        }

        return isCreated;
    }

    public async Task<Employee?> UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new UpdateEmployeeCommand
        {
            Employee = employee
        }, cancellationToken);

        if (result is not null)
        {
            await _cacheService.RemoveAsync(CacheKeys.Employees.GetAll, cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Employees.Get(employee.Id), cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Employees.GetByEmail(employee.Email), cancellationToken);
        }

        return result;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        bool isDeleted = await _sender.Send(new DeleteEmployeeByIdCommand
        {
            Id = id
        }, cancellationToken);

        if (isDeleted)
        {
            await _cacheService.RemoveAsync(CacheKeys.Employees.GetAll, cancellationToken);
            await _cacheService.RemoveAsync(CacheKeys.Employees.Get(id), cancellationToken);
        }

        return isDeleted;
    }
}