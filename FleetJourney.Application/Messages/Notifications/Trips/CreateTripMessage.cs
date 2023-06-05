﻿using FleetJourney.Domain.Trips;
using Mediator;

namespace FleetJourney.Application.Messages.Notifications.Trips;

public sealed class CreateTripMessage : INotification
{
    public required Trip Trip { get; init; }
}