namespace FleetJourney.Domain.Messages.Employees;

public sealed class CreateEmployee
{
    public required string Email { get; init; }
    
    public required string Name { get; init; }
    
    public required string LastName { get; init; }
    
    public required DateOnly Birthdate { get; init; }
}