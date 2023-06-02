namespace FleetJourney.Messages.Employees;

public sealed class RegisterNewEmployee
{
    public required string Name { get; init; }

    public required string Email { get; init; }
}