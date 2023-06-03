namespace FleetJourney.Core.Contracts.Responses.Employees;

public sealed class EmployeeResponse
{
    public required Guid Id { get; init; }  
    
    public required string Name { get; init; }
    
    public required string Email { get; init; }
    
    public required DateOnly Birthdate { get; init; }
}