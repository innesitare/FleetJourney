using Mediator;

namespace FleetJourney.Core.Messages.Commands.Employees;

public sealed class DeleteEmployeeByIdCommand : ICommand<bool>
{
    public required Guid Id { get; init; }
}