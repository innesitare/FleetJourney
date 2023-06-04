using FleetJourney.Domain.EmployeeInfo;

namespace FleetJourney.Application.Services.Abstractions;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken);

    Task<Employee?> GetAsync(Guid employeeId, CancellationToken cancellationToken);
    
    Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    
    Task<bool> CreateAsync(Employee employee, CancellationToken cancellationToken);
    
    Task<Employee?> UpdateAsync(Employee employee, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(Guid employeeId, CancellationToken cancellationToken);
}