using FleetJourney.Application.Contracts.Requests.Trips;
using FleetJourney.Application.Contracts.Responses.Trips;
using FleetJourney.Domain.Trips;
using Riok.Mapperly.Abstractions;

namespace FleetJourney.Application.Mapping;

[Mapper]
public static partial class TripMapper
{
    public static partial TripResponse ToResponse(this Trip trip);

    public static partial Trip ToTrip(this CreateTripRequest request);
    
    public static partial Trip ToTrip(this UpdateTripRequest request);

    public static Trip ToTrip(this UpdateTripRequest request, Guid id)
    {
        request.Id = id;
        return request.ToTrip();
    }
}