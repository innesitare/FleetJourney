using FleetJourney.Domain.EmployeeInfo;

namespace FleetJourney.Domain.Trips;

public record struct TripId(Guid Value);

public sealed class Trip
{
    public required TripId Id { get; init; }

    public required string LicensePlateNumber { get; init; }
    
    public required uint StartMileage { get; init; }
    
    public required uint EndMileage { get; init; }
    
    public required bool IsPrivateTrip { get; init; }
    
    public required Employee? Employee { get; init; }
}