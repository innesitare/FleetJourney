namespace FleetJourney.Domain.Messages.Employees;

public sealed class DeleteEmployee
{
    public required Guid Id { get; init; }
}