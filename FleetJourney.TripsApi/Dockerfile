FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FleetJourney.TripsApi/FleetJourney.TripsApi.csproj", "FleetJourney.TripsApi/"]
COPY ["FleetJourney.Application/FleetJourney.Application.csproj", "FleetJourney.Application/"]
COPY ["FleetJourney.Infrastructure/FleetJourney.Infrastructure.csproj", "FleetJourney.Infrastructure/"]
COPY ["FleetJourney.Domain/FleetJourney.Domain.csproj", "FleetJourney.Domain/"]
RUN dotnet restore "FleetJourney.TripsApi/FleetJourney.TripsApi.csproj"
COPY . .
WORKDIR "/src/FleetJourney.TripsApi"
RUN dotnet build "FleetJourney.TripsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetJourney.TripsApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetJourney.TripsApi.dll"]
