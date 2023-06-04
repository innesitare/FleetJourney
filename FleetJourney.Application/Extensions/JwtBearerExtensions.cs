using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FleetJourney.Application.Extensions;

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