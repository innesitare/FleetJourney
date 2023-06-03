using FleetJourney.Domain.CarPool;

namespace FleetJourney.Domain.Messages.CarPool;

public sealed class UpdateCar
{
    public required Car Car { get; init; }
}