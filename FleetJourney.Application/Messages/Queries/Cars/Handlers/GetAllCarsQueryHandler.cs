using FleetJourney.Application.Repositories.Abstractions;
using FleetJourney.Domain.CarPool;
using Mediator;

namespace FleetJourney.Application.Messages.Queries.Cars.Handlers;

public sealed class GetAllCarsQueryHandler : IQueryHandler<GetAllCarsQuery, IEnumerable<Car>>
{
    private readonly ICarPoolRepository _carPoolRepository;

    public GetAllCarsQueryHandler(ICarPoolRepository carPoolRepository)
    {
        _carPoolRepository = carPoolRepository;
    }


    public async ValueTask<IEnumerable<Car>> Handle(GetAllCarsQuery query, CancellationToken cancellationToken)
    {
        var cars = await _carPoolRepository.GetAllAsync(cancellationToken);

        return cars;
    }
}
