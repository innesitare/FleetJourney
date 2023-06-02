FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FleetJourney.IdentityService/FleetJourney.IdentityService.csproj", "FleetJourney.IdentityService/"]
COPY ["FleetJourney.Core/FleetJourney.Core.csproj", "FleetJourney.Core/"]
COPY ["FleetJourney.Messages/FleetJourney.Messages.csproj", "FleetJourney.Messages/"]
COPY ["FleetJourney.Domain/FleetJourney.Domain.csproj", "FleetJourney.Domain/"]
RUN dotnet restore "FleetJourney.IdentityService/FleetJourney.IdentityService.csproj"
COPY . .
WORKDIR "/src/FleetJourney.IdentityService"
RUN dotnet build "FleetJourney.IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetJourney.IdentityService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetJourney.IdentityService.dll"]
