using Mediator;

namespace FleetJourney.Application.Messages.Commands.Employees;

public sealed class DeleteEmployeeCommand : ICommand<bool>
{
    public required Guid Id { get; init; }
}