FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FleetJourney.CarService/FleetJourney.CarService.csproj", "FleetJourney.CarService/"]
RUN dotnet restore "FleetJourney.CarService/FleetJourney.CarService.csproj"
COPY . .
WORKDIR "/src/FleetJourney.CarService"
RUN dotnet build "FleetJourney.CarService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FleetJourney.CarService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FleetJourney.CarService.dll"]
