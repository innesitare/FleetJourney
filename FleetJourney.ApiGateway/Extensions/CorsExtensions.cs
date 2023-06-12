using Microsoft.Net.Http.Headers;

namespace FleetJourney.ApiGateway.Extensions;

public static class CorsExtensions
{
    public static IConfigurationBuilder AddCors(this IConfigurationBuilder configuration,
        WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins(builder.Configuration["Client:OriginUrl"]!)
                    .WithHeaders(HeaderNames.ContentType, HeaderNames.Authorization)
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(86400))
                    .AllowAnyMethod();
            });
        });
        
        return builder.Configuration;
    }
}