using FleetJourney.Domain.CarPool;
using FleetJourney.Domain.EmployeeInfo;

namespace FleetJourney.Domain.Trips;

public sealed class Trip
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public required string LicensePlateNumber { get; init; }
    
    public required uint StartMileage { get; init; }
    
    public required uint EndMileage { get; init; }
    
    public required bool IsPrivateTrip { get; init; }
    
    public required Guid EmployeeId { get; init; }
}