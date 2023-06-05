namespace FleetJourney.Domain.Messages.CarPool;

public sealed class DeleteCar
{
    public required Guid Id { get; init; }
}