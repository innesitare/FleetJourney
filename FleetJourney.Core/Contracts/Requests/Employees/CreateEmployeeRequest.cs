namespace FleetJourney.Core.Contracts.Requests.Employees;

public sealed class CreateEmployeeRequest
{
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }
    
    public required DateOnly Birthdate { get; init; }
}