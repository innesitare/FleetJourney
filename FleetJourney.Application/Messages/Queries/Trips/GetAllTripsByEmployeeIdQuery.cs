using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips;

public sealed class GetAllTripsByEmployeeIdQuery : IQuery<IEnumerable<Trip>>
{
    public required Guid EmployeeId { get; init; }
}