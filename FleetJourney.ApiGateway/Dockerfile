FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FleetJourney.ApiGateway/FleetJourney.ApiGateway.csproj", "FleetJourney.ApiGateway/"]
RUN dotnet restore "FleetJourney.ApiGateway/FleetJourney.ApiGateway.csproj"
COPY . .
WORKDIR "/src/FleetJourney.ApiGateway"
RUN dotnet build "FleetJourney.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetJourney.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetJourney.ApiGateway.dll"]
