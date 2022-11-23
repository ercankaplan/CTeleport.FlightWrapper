#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CTeleport.FlightWrapper.Api/CTeleport.FlightWrapper.Api.csproj", "CTeleport.FlightWrapper.Api/"]
RUN dotnet restore "CTeleport.FlightWrapper.Api/CTeleport.FlightWrapper.Api.csproj"
COPY . .
WORKDIR "/src/CTeleport.FlightWrapper.Api"
RUN dotnet build "CTeleport.FlightWrapper.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CTeleport.FlightWrapper.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CTeleport.FlightWrapper.Api.dll"]