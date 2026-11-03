# syntax=docker/dockerfile:1
#
# NAWWAR API — multi-stage build.
#   dev      : SDK image running `dotnet watch` against bind-mounted source (compose dev)
#   test     : restores, builds and runs the test suite (`docker build --target test`)
#   runtime  : minimal ASP.NET Core image, non-root, production default (final stage)
#
# Stage order matters for the legacy (non-BuildKit) builder, which builds every
# stage up to the target: `dev` comes first so dev builds skip compile/test, and
# `runtime` builds run the test stage as a quality gate. BuildKit skips unused stages.
#
# No secrets are baked into any stage; configuration comes from the environment.

ARG DOTNET_VERSION=10.0

# ---------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION}-alpine AS sdk
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    DOTNET_NOLOGO=1 \
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
# ICU is required for correct Arabic/French culture data (no invariant mode).
RUN apk add --no-cache icu-libs icu-data-full
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /src

# ---------------------------------------------------------------------------
# Development: source is bind-mounted at /src by docker-compose.dev.yml.
# bin/obj and the NuGet cache live in named volumes owned by the non-root
# `app` user (UID 1654, provided by the .NET base image).
FROM sdk AS dev
RUN mkdir -p /src/src/Nawwar.Api/bin /src/src/Nawwar.Api/obj \
             /src/tests/Nawwar.Api.Tests/bin /src/tests/Nawwar.Api.Tests/obj \
             /home/app/.nuget/packages \
 && chown -R app:app /src /home/app
USER app
ENV ASPNETCORE_ENVIRONMENT=Development \
    ASPNETCORE_HTTP_PORTS=8080 \
    DOTNET_WATCH_SUPPRESS_LAUNCH_BROWSER=1 \
    DOTNET_WATCH_RESTART_ON_RUDE_EDIT=1
EXPOSE 8080
CMD ["dotnet", "watch", "--project", "src/Nawwar.Api/Nawwar.Api.csproj", "--non-interactive", "run", "--no-launch-profile"]

# ---------------------------------------------------------------------------
FROM sdk AS restore
# Copy only project metadata first so the restore layer is cached until
# dependencies change.
COPY global.json Nawwar.slnx Directory.Build.props Directory.Packages.props ./
COPY src/Nawwar.Api/Nawwar.Api.csproj src/Nawwar.Api/
COPY tests/Nawwar.Api.Tests/Nawwar.Api.Tests.csproj tests/Nawwar.Api.Tests/
RUN dotnet restore Nawwar.slnx

# ---------------------------------------------------------------------------
FROM restore AS build
COPY . .
RUN dotnet build Nawwar.slnx -c Release --no-restore

# ---------------------------------------------------------------------------
FROM build AS test
RUN dotnet test Nawwar.slnx -c Release --no-build

# ---------------------------------------------------------------------------
FROM build AS publish
RUN dotnet publish src/Nawwar.Api/Nawwar.Api.csproj -c Release --no-build \
      -o /app/publish /p:UseAppHost=false

# ---------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine AS runtime
RUN apk add --no-cache icu-libs icu-data-full tzdata
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    ASPNETCORE_ENVIRONMENT=Production \
    ASPNETCORE_HTTP_PORTS=8080 \
    TZ=UTC
WORKDIR /app
# Application files stay root-owned and read-only for the runtime user.
COPY --from=publish /app/publish .
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "Nawwar.Api.dll"]
