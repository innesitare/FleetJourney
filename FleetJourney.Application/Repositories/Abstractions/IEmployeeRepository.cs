using FleetJourney.Domain.EmployeeInfo;

namespace FleetJourney.Application.Repositories.Abstractions;

public interface IEmployeeRepository : IRepository<Employee, Guid>
{
    Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}