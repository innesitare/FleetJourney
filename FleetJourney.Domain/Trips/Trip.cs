using FleetJourney.Domain.CarPool;
using FleetJourney.Domain.EmployeeInfo;

namespace FleetJourney.Domain.Trips;

public sealed class Trip
{
    public required Guid Id { get; init; }

    public required string LicensePlateNumber { get; init; }
    
    public required uint StartMileage { get; init; }
    
    public required uint EndMileage { get; init; }
    
    public required bool IsPrivateTrip { get; init; }
    
    public required Guid EmployeeId { get; init; }
    
    public required Employee? Employee { get; init; }
    
    public required Car? Car { get; init; }
}