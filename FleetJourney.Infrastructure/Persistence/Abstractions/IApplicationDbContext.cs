using FleetJourney.Domain.CarPool;
using FleetJourney.Domain.EmployeeInfo;
using FleetJourney.Domain.Trips;
using Microsoft.EntityFrameworkCore;

namespace FleetJourney.Infrastructure.Persistence.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; init; }
    
    DbSet<Trip> Trips { get; init; }
    
    DbSet<Car> Cars { get; init; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}