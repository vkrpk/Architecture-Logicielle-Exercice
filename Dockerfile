﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN apt-get update && apt-get install -y netcat-traditional

RUN useradd -m appuser
USER appuser

USER root

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN dotnet tool install --global dotnet-ef --version 8.0.3
ENV PATH="${PATH}:/root/.dotnet/tools"
ENV DB_HOST=appdb
ENV DB_PORT=1433
ENV DB_NAME=dbname
ENV DB_PASSWORD=password@12345#
RUN apt-get update && apt-get install -y netcat-traditional
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Architecture/Architecture.csproj", "Architecture/"]
COPY ["Architecture.Impl.EFDatabase/Architecture.Impl.EFDatabase.csproj", "Architecture.Impl.EFDatabase/"]
RUN dotnet restore "Architecture/Architecture.csproj"
RUN dotnet restore "Architecture.Impl.EFDatabase/Architecture.Impl.EFDatabase.csproj"
COPY . .
COPY ["wait-for-it.sh", "/wait-for-it/wait-for-it.sh"]
WORKDIR "/src/Architecture.Impl.EFDatabase"
RUN useradd -m appuser
USER root
RUN chmod +x /wait-for-it/wait-for-it.sh
CMD ["/wait-for-it/wait-for-it.sh", "app-db:1433", "--", "dotnet", "ef", "database", "update"]
RUN mkdir -p /app/build && chown -R appuser:appuser /app
WORKDIR "/src/Architecture"
RUN dotnet build "Architecture.csproj" -c $BUILD_CONFIGURATION -o /app/build

#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "Architecture.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#
#USER root
#RUN mkdir -p /home/app/.aspnet/DataProtection-Keys && \
#    chown -R appuser:appuser /home/app/.aspnet && \
#    chmod -R 700 /home/app/.aspnet/DataProtection-Keys
#USER appuser
#
#ENTRYPOINT ["dotnet", "Architecture.dll"]
