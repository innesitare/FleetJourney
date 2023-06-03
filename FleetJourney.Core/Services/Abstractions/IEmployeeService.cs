using FleetJourney.Domain.EmployeeInfo;

namespace FleetJourney.Core.Services.Abstractions;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken);

    Task<Employee?> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken);
    
    Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    
    Task<bool> CreateAsync(Employee employee, CancellationToken cancellationToken);
    
    Task<Employee?> UpdateAsync(Employee employee, CancellationToken cancellationToken);
    
    Task<bool> DeleteByIdAsync(Guid employeeId, CancellationToken cancellationToken);
}