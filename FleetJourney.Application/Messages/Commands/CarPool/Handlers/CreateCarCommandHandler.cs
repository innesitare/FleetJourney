using FleetJourney.Application.Repositories.Abstractions;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.CarPool.Handlers;

public sealed class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, bool>
{
    private readonly ICarPoolRepository _carPoolRepository;

    public CreateCarCommandHandler(ICarPoolRepository carPoolRepository)
    {
        _carPoolRepository = carPoolRepository;
    }

    public async ValueTask<bool> Handle(CreateCarCommand command, CancellationToken cancellationToken)
    {
        bool created = await _carPoolRepository.CreateAsync(command.Car, cancellationToken);

        return created;
    }
}