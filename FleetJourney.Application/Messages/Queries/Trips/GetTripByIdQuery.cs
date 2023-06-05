using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips;

public sealed class GetTripByIdQuery : IQuery<Trip?>
{
    public required Guid Id { get; init; }
}