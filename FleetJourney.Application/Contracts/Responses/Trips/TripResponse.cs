namespace FleetJourney.Application.Contracts.Responses.Trips;

public sealed class TripResponse
{
    public required Guid Id { get; init; }

    public required uint StartMileage { get; init; }
    
    public required uint EndMileage { get; init; }
    
    public required bool IsPrivateTrip { get; init; }
    
    public required Guid EmployeeId { get; init; }
    
    public required Guid CarId { get; init; }
}