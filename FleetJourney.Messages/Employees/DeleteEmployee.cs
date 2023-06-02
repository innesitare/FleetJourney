namespace FleetJourney.Messages.Employees;

public sealed class DeleteEmployee
{
    public required string Name { get; init; }

    public required string Email { get; init; }
}