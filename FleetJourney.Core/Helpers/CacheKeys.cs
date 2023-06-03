namespace FleetJourney.Core.Helpers;

internal static class CacheKeys
{
    public static class Employees
    {
        public static string GetAll => "employees-all";
        public static string Get(Guid employeeId) => $"employees-{employeeId}";
        public static string GetByEmail(string email) => $"employees-email-{email}";
    }
}