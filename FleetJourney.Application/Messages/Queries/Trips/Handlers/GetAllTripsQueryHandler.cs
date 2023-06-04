using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Trips.Handlers;

public sealed class GetAllTripsQueryHandler : IQueryHandler<GetAllTripsQuery, IEnumerable<Trip>>
{
    private readonly ITripRepository _tripRepository;

    public GetAllTripsQueryHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async ValueTask<IEnumerable<Trip>> Handle(GetAllTripsQuery query, CancellationToken cancellationToken)
    {
        var trips = await _tripRepository.GetAllAsync(cancellationToken);

        return trips;
    }
}