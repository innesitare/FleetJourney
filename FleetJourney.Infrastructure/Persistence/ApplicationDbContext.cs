using System.Reflection;
using FleetJourney.Domain.CarPool;
using FleetJourney.Domain.EmployeeInfo;
using FleetJourney.Domain.Trips;
using FleetJourney.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FleetJourney.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public required DbSet<Employee> Employees { get; init; }
    public required DbSet<Trip> Trips { get; init; }
    public required DbSet<Car> Cars { get; init; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}