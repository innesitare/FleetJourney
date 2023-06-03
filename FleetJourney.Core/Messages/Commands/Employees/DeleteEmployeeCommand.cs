using Mediator;

namespace FleetJourney.Core.Messages.Commands.Employees;

public sealed class DeleteEmployeeCommand : ICommand<bool>
{
    public required Guid Id { get; init; }
}