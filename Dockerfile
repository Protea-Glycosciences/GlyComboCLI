FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build

WORKDIR /src

COPY GlyComboCLI/GlyComboCLI.csproj GlyComboCLI/
RUN dotnet restore GlyComboCLI/GlyComboCLI.csproj

COPY GlyComboCLI/ GlyComboCLI/

RUN dotnet publish GlyComboCLI/GlyComboCLI.csproj \
    --configuration Release \
    --framework net8.0 \
    --runtime linux-x64 \
    --self-contained false \
    -p:DebugType=None \
    -p:DebugSymbols=false \
    --output /app/publish


FROM mcr.microsoft.com/dotnet/runtime:8.0-bookworm-slim AS runtime

WORKDIR /app

COPY --from=build /app/publish .