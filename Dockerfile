FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

RUN useradd -m appuser

USER appuser

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Architecture/Architecture.csproj", "Architecture/"]
RUN dotnet restore "Architecture/Architecture.csproj"
COPY . .
WORKDIR "/src/Architecture"
RUN dotnet build "Architecture.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Architecture.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER root
RUN mkdir -p /home/app/.aspnet/DataProtection-Keys && \
    chown -R appuser:appuser /home/app/.aspnet && \
    chmod -R 700 /home/app/.aspnet/DataProtection-Keys
USER appuser

ENTRYPOINT ["dotnet", "Architecture.dll"]