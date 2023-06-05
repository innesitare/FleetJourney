﻿namespace FleetJourney.Application.Contracts.Requests.Trips;

public sealed class CreateTripRequest
{
    public required uint StartMileage { get; init; }
    
    public required uint EndMileage { get; init; }
    
    public required bool IsPrivateTrip { get; init; }
    
    public required Guid EmployeeId { get; init; }
    
    public required Guid CarId { get; init; }
}