using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips;

public sealed class GetTripByCarIdQuery : IQuery<Trip?>
{
    public required Guid CarId { get; init; }
}