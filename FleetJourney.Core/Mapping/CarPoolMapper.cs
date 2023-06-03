using FleetJourney.Core.Contracts.Requests.CarPool;
using FleetJourney.Core.Contracts.Responses.CarPool;
using FleetJourney.Domain.CarPool;
using Riok.Mapperly.Abstractions;

namespace FleetJourney.Core.Mapping;

[Mapper]
public static partial class CarPoolMapper
{
    public static partial CarResponse ToResponse(this Car car);

    public static partial Car ToCar(this CreateCarRequest request);

    public static partial Car ToCar(this UpdateCarRequest request);

    public static Car ToCar(this UpdateCarRequest request, string licensePlateNumber)
    {
        request.LicensePlateNumber = licensePlateNumber;
        return request.ToCar();
    }
}