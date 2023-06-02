namespace FleetJourney.Domain.CarPool;

public record struct CarId(Guid Value);

public sealed class Car
{
    public required CarId Id { get; init; }
    
    public required string LicensePlateNumber { get; init; }
    
    public required string Brand { get; init; }
    
    public required string Model { get; init; }
    
    public required uint EndOfLifeMileage { get; init; }
    
    public required uint MaintenanceInterval { get; init; }
    
    public required uint CurrentMileage { get; set; }
}