FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["restaurant-api/Restaurant.API/Restaurant.API.csproj", "./"]
COPY ["restaurant-api/Restaurant.Application/Restaurant.Application.csproj", "./"]
COPY ["restaurant-api/Restaurant.Core/Restaurant.Core.csproj", "./"]
COPY ["restaurant-api/Restaurant.Infrastructure/Restaurant.Infrastructure.csproj", "./"]
RUN dotnet restore "Restaurant.API.csproj"
COPY . .
WORKDIR "/src/restaurant-api/Restaurant.API"
RUN dotnet build "Restaurant.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Restaurant.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Restaurant.API.dll"]