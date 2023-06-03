using FleetJourney.Domain.Trips;

namespace FleetJourney.Domain.EmployeeInfo;

public sealed class Employee
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }
    
    public required DateOnly Birthdate { get; init; }
    
    public IEnumerable<Trip>? Trips { get; init; }
}