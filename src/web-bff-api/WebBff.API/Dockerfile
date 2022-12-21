FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["web-bff-api/WebBff.API/WebBff.API.csproj", "./"]
RUN dotnet restore "WebBff.API.csproj"
COPY . .
WORKDIR "/src/web-bff-api/WebBff.API"
RUN dotnet build "WebBff.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebBff.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebBff.API.dll"]