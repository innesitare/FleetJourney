using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips;

public sealed class GetTripByCarPlateNumberQuery : IQuery<Trip?>
{
    public required string LicensePlateNumber { get; init; }
}