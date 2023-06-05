using FleetJourney.Application.Messages.Notifications.CarPool;
using FleetJourney.Application.Repositories.Abstractions;
using Mediator;

namespace FleetJourney.Application.Messages.Commands.CarPool.Handlers;

public sealed class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, bool>
{
    private readonly ICarPoolRepository _carPoolRepository;
    private readonly IPublisher _publisher;

    public CreateCarCommandHandler(ICarPoolRepository carPoolRepository, IPublisher publisher)
    {
        _carPoolRepository = carPoolRepository;
        _publisher = publisher;
    }

    public async ValueTask<bool> Handle(CreateCarCommand command, CancellationToken cancellationToken)
    {
        bool created = await _carPoolRepository.CreateAsync(command.Car, cancellationToken);
        if (created)
        {
            await _publisher.Publish(new CreateCarMessage
            {
                Car = command.Car
            }, cancellationToken);
        }
        
        return created;
    }
}