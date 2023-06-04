namespace FleetJourney.Domain.Messages.Trips;

public sealed class CreateTrip
{
    public required string LicensePlateNumber { get; init; }
    
    public required uint StartMileage { get; init; }
    
    public required uint EndMileage { get; init; }
    
    public required bool IsPrivateTrip { get; init; }
    
    public required Guid EmployeeId { get; init; }
}