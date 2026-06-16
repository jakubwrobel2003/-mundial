FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY MundialPrediction.Core/MundialPrediction.Core.csproj           MundialPrediction.Core/
COPY MundialPrediction.Infrastructure/MundialPrediction.Infrastructure.csproj MundialPrediction.Infrastructure/
COPY MundialPrediction.API/MundialPrediction.API.csproj             MundialPrediction.API/

RUN dotnet restore MundialPrediction.API/MundialPrediction.API.csproj

COPY . .
RUN dotnet publish MundialPrediction.API/MundialPrediction.API.csproj \
    -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

RUN apt-get update && apt-get install -y --no-install-recommends \
    libsqlite3-dev ca-certificates \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# Railway wstrzykuje PORT; ASP.NET Core czyta ASPNETCORE_URLS
ENV ASPNETCORE_URLS=http://+:${PORT:-5000}
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DATABASE_PATH=/data/mundial2026.db

EXPOSE 5000
ENTRYPOINT ["dotnet", "MundialPrediction.API.dll"]
