using FleetJourney.Domain.EmployeeInfo;

namespace FleetJourney.Infrastructure.Repositories.Abstractions;

public interface IEmployeeRepository : IRepository<Employee, Guid>
{
    Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}