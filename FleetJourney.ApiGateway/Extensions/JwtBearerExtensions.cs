using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FleetJourney.ApiGateway.Extensions;

public static class JwtBearerExtensions
{
    public static IConfigurationBuilder AddJwtBearer(this IConfigurationBuilder config, WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = builder.Configuration["Auth0:Audience"],
                    ValidIssuer = $"{builder.Configuration["Auth0:Domain"]}"
                };
            });

        builder.Services.AddAuthorization(o =>
        {
            o.AddPolicy("fleetjourney:read-write",
                policyBuilder =>
                {
                    policyBuilder.RequireAuthenticatedUser().RequireClaim("scope", "fleetjourney:read-write");
                });
        });

        return builder.Configuration;
    }
}