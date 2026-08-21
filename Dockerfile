# Build GlyComboCLI from source
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build

WORKDIR /src

# Copy project definition first so dependency restoration can be cached
COPY GlyComboCLI/GlyComboCLI.csproj GlyComboCLI/

RUN dotnet restore GlyComboCLI/GlyComboCLI.csproj

# Copy the remaining source
COPY GlyComboCLI/ GlyComboCLI/

RUN dotnet publish GlyComboCLI/GlyComboCLI.csproj \
    --configuration Release \
    --framework net8.0 \
    --self-contained false \
    -p:DebugType=None \
    -p:DebugSymbols=false \
    --output /app/publish


# Runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0-bookworm-slim AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["/app/GlyComboCLI"]
