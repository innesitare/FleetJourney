FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FleetJourney.EmployeeApi/FleetJourney.EmployeeApi.csproj", "FleetJourney.EmployeeApi/"]
COPY ["FleetJourney.Core/FleetJourney.Core.csproj", "FleetJourney.Core/"]
COPY ["FleetJourney.Domain/FleetJourney.Domain.csproj", "FleetJourney.Domain/"]
COPY ["FleetJourney.Infrastructure/FleetJourney.Infrastructure.csproj", "FleetJourney.Infrastructure/"]
RUN dotnet restore "FleetJourney.EmployeeApi/FleetJourney.EmployeeApi.csproj"
COPY . .
WORKDIR "/src/FleetJourney.EmployeeApi"
RUN dotnet build "FleetJourney.EmployeeApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetJourney.EmployeeApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetJourney.EmployeeApi.dll"]
