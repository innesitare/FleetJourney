using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Cars.Handlers;

public class GetCarByIdQueryHandler : IQueryHandler<GetCarByIdQuery, Car?>
{
    private readonly ICarPoolRepository _carPoolRepository;

    public GetCarByIdQueryHandler(ICarPoolRepository carPoolRepository)
    {
        _carPoolRepository = carPoolRepository;
    }

    public async ValueTask<Car?> Handle(GetCarByIdQuery query, CancellationToken cancellationToken)
    {
        var cars = await _carPoolRepository.GetAsync(query.Id, cancellationToken);

        return cars;
    }
}