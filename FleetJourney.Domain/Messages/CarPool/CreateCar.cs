namespace FleetJourney.Domain.Messages.CarPool;

public sealed class CreateCar
{
    public required string LicensePlateNumber { get; init; }
    
    public required string Brand { get; init; }
    
    public required string Model { get; init; }
    
    public required uint EndOfLifeMileage { get; init; }
    
    public required uint MaintenanceInterval { get; init; }
    
    public required uint CurrentMileage { get; init; }
}