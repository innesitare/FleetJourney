using FleetJourney.Application.Extensions;
using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.EmployeeInfo;
using FleetJourney.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FleetJourney.Application.Repositories;

internal sealed class EmployeeRepository : IEmployeeRepository
{
    private readonly IApplicationDbContext _applicationDbContext;

    public EmployeeRepository(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
    {
        bool isEmpty = await _applicationDbContext.Employees.AnyAsync(cancellationToken);
        if (!isEmpty)
        {
            return Enumerable.Empty<Employee>();
        }
        
        return _applicationDbContext.Employees.Include(e => e.Trips);
    }

    public async Task<Employee?> GetAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await _applicationDbContext.Employees.FindAsync(new object?[] {employeeId}, cancellationToken);
        if (employee is null)
        {
            return null;
        }

        await _applicationDbContext.Employees.LoadDataAsync(employee, e => e.Trips);
        return employee;
    }

    public async Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var employee = await _applicationDbContext.Employees
            .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);

        await _applicationDbContext.Employees.LoadDataAsync(employee!, e => e.Trips);
        return employee;
    }

    public async Task<bool> CreateAsync(Employee employee, CancellationToken cancellationToken)
    {
        await _applicationDbContext.Employees.AddAsync(employee, cancellationToken);
        int result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        return result > 0;
    }

    public async Task<Employee?> UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        bool isContains = await _applicationDbContext.Employees.ContainsAsync(employee, cancellationToken);
        if (!isContains)
        {
            return null;
        }

        _applicationDbContext.Employees.Update(employee);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return employee;
    }

    public async Task<bool> DeleteAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        int result = await _applicationDbContext.Employees
            .Where(e => e.Id == employeeId)
            .ExecuteDeleteAsync(cancellationToken);

        return result > 0;
    }
}