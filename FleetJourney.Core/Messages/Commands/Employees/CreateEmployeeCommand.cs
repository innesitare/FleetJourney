﻿using FleetJourney.Domain.EmployeeInfo;
using Mediator;

namespace FleetJourney.Core.Messages.Commands.Employees;

public sealed class CreateEmployeeCommand : ICommand<bool>
{
    public required Employee Employee { get; init; }
}