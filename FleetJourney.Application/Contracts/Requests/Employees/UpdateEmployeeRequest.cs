namespace FleetJourney.Application.Contracts.Requests.Employees;

public sealed class UpdateEmployeeRequest
{
    public Guid Id { get; internal set; }
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }
    
    public required DateOnly Birthdate { get; init; }
}