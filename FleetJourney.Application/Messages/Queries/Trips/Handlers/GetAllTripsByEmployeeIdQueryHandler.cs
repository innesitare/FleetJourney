using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips.Handlers;

public sealed class GetAllTripsByEmployeeIdQueryHandler : IQueryHandler<GetAllTripsByEmployeeIdQuery, IEnumerable<Trip>>
{
    private readonly ITripRepository _tripRepository;

    public GetAllTripsByEmployeeIdQueryHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async ValueTask<IEnumerable<Trip>> Handle(GetAllTripsByEmployeeIdQuery query, CancellationToken cancellationToken)
    {
        var trips = await _tripRepository.GetAllByEmployeeIdAsync(query.EmployeeId, cancellationToken);

        return trips;
    }
}