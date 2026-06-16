# ── Stage 1: Build Angular ────────────────────────────────────────────────────
FROM node:20-alpine AS frontend-build
WORKDIR /app
COPY frontend/mundial-frontend/package*.json ./
RUN npm ci --prefer-offline
COPY frontend/mundial-frontend/ .
RUN npm run build

# ── Stage 2: Build .NET ───────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS backend-build
WORKDIR /src
COPY backend/MundialPrediction.Core/MundialPrediction.Core.csproj MundialPrediction.Core/
COPY backend/MundialPrediction.Infrastructure/MundialPrediction.Infrastructure.csproj MundialPrediction.Infrastructure/
COPY backend/MundialPrediction.API/MundialPrediction.API.csproj MundialPrediction.API/
RUN dotnet restore MundialPrediction.API/MundialPrediction.API.csproj
COPY backend/ .
RUN dotnet publish MundialPrediction.API/MundialPrediction.API.csproj \
    -c Release -o /app/publish --no-restore

# ── Stage 3: Runtime ──────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

RUN apt-get update \
 && apt-get install -y --no-install-recommends libsqlite3-dev ca-certificates \
 && rm -rf /var/lib/apt/lists/*

# Backend binaries
COPY --from=backend-build /app/publish .

# Angular SPA → wwwroot (serwowane przez ASP.NET Core Static Files)
COPY --from=frontend-build /app/dist/mundial-frontend/browser ./wwwroot

ENV ASPNETCORE_ENVIRONMENT=Production
ENV DATABASE_PATH=/data/mundial2026.db

EXPOSE 8080
ENTRYPOINT ["dotnet", "MundialPrediction.API.dll"]
