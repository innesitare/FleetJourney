﻿using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Core.Messages.Commands.Employees;

public sealed class UpdateEmployeeCommand : ICommand<Employee?>
{
    public required Employee Employee { get; init; }
}