using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Core.Messages.Queries.Cars;

public sealed class GetCarByNumberQuery : IQuery<Car?>
{
    public required string LicensePlateNumber { get; init; }
}