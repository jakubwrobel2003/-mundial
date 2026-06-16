# ── Stage 1: Build Angular ────────────────────────────────────────────────────
FROM node:20-alpine AS frontend-build
WORKDIR /app
COPY frontend/mundial-frontend/package*.json ./
RUN npm ci
COPY frontend/mundial-frontend/ .
RUN npm run build

# Normalizuj output – działa bez względu na to czy Angular stworzył browser/ czy nie
RUN INDEX=$(find dist -name "index.html" | head -1) && \
    echo "==> index.html found at: $INDEX" && \
    SPA_DIR=$(dirname "$INDEX") && \
    echo "==> Copying from: $SPA_DIR" && \
    cp -r "$SPA_DIR" /spa && \
    echo "=== /spa contents ===" && ls /spa

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

COPY --from=backend-build /app/publish .
COPY --from=frontend-build /spa ./wwwroot

ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_ENVIRONMENT=Production
ENV DATABASE_PATH=/data/mundial2026.db

EXPOSE 8080
ENTRYPOINT ["dotnet", "MundialPrediction.API.dll"]
