namespace FleetJourney.Domain.Messages.CarPool;

public sealed class DeleteCar
{
    public required string LicensePlateNumber { get; init; }
}