using FleetJourney.Application.Contracts.Requests.CarPool;
using FleetJourney.Application.Contracts.Responses.CarPool;
using FleetJourney.Domain.CarPool;
using Riok.Mapperly.Abstractions;

namespace FleetJourney.Application.Mapping;

[Mapper]
public static partial class CarPoolMapper
{
    public static partial CarResponse ToResponse(this Car car);

    public static partial Car ToCar(this CreateCarRequest request);

    public static partial Car ToCar(this UpdateCarRequest request);

    public static Car ToCar(this UpdateCarRequest request, Guid carId)
    {
        request.Id = carId;
        return request.ToCar();
    }
}