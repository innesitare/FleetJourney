using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Cars.Handlers;

public class GetCarByNumberQueryHandler : IQueryHandler<GetCarByNumberQuery, Car?>
{
    private readonly ICarPoolRepository _carPoolRepository;

    public GetCarByNumberQueryHandler(ICarPoolRepository carPoolRepository)
    {
        _carPoolRepository = carPoolRepository;
    }

    public async ValueTask<Car?> Handle(GetCarByNumberQuery query, CancellationToken cancellationToken)
    {
        var cars = await _carPoolRepository.GetAsync(query.LicensePlateNumber, cancellationToken);

        return cars;
    }
}