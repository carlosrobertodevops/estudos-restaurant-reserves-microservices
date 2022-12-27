FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["security-api/Security.API/Security.API.csproj", "./"]
RUN dotnet restore "Security.API.csproj"
COPY . .
WORKDIR "/src/security-api/Security.API"
RUN dotnet build "Security.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Security.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Security.API.dll"]