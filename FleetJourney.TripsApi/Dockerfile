FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FleetJourney.TripService/FleetJourney.TripService.csproj", "FleetJourney.TripService/"]
RUN dotnet restore "FleetJourney.TripService/FleetJourney.TripService.csproj"
COPY . .
WORKDIR "/src/FleetJourney.TripService"
RUN dotnet build "FleetJourney.TripService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetJourney.TripService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetJourney.TripService.dll"]
