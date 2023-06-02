namespace FleetJourney.Domain.EmployeeInfo;

public record struct EmployeeId(Guid Value);

public sealed class Employee
{
    public required EmployeeId Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }
}