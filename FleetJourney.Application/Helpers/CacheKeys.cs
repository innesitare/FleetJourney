namespace FleetJourney.Application.Helpers;

public static class CacheKeys
{
    public static class Employees
    {
        public static string GetAll => "employees-all";
        public static string Get(Guid employeeId) => $"employees-{employeeId}";
        public static string GetByEmail(string email) => $"employees-email-{email}";
    }

    public static class CarPool
    {
        public static string GetAll => "cars-all";
        public static string Get(Guid carId) => $"cars-{carId}";
    }

    public static class Trips
    {
        public static string GetAll => "trips-all";
        public static string GetAllByEmployeeId(Guid employeeId) => $"trips-{employeeId}-all";
        public static string Get(Guid tripId) => $"trips-{tripId}";
        public static string GetByCarId(Guid carId) => $"trips-{carId}";
    }
}