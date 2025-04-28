# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["microservices/Support/Support.API/Support.API.csproj", "microservices/Support/Support.API/"]
COPY ["microservices/Support/Application/Application.csproj", "microservices/Support/Application/"]
COPY ["microservices/Support/Domain/Domain.csproj", "microservices/Support/Domain/"]
COPY ["buildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "buildingBlocks/BuildingBlocks/"]
COPY ["microservices/Support/Infrastucture/Infrastructure.csproj", "microservices/Support/Infrastucture/"]
RUN dotnet restore "./microservices/Support/Support.API/Support.API.csproj"
COPY . .
WORKDIR "/src/microservices/Support/Support.API"
RUN dotnet build "./Support.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Support.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Support.API.dll"]