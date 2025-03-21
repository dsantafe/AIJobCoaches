#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

# Instalar Chrome y WebDriver
RUN apt-get update && apt-get install -y \
    wget unzip curl && \
    wget -q https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb && \
    apt-get install -y ./google-chrome-stable_current_amd64.deb && \
    wget -q https://chromedriver.storage.googleapis.com/134.0.XXX/chromedriver_linux64.zip && \
    unzip chromedriver_linux64.zip && \
    mv chromedriver /usr/local/bin/ && \
    chmod +x /usr/local/bin/chromedriver

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AIJobCoaches.Worker/AIJobCoaches.Worker.csproj", "AIJobCoaches.Worker/"]
COPY ["AIJobCoaches.Application/AIJobCoaches.Application.csproj", "AIJobCoaches.Application/"]
COPY ["AIJobCoaches.Domain/AIJobCoaches.Domain.csproj", "AIJobCoaches.Domain/"]
COPY ["AIJobCoaches.Infrastructure/AIJobCoaches.Infrastructure.csproj", "AIJobCoaches.Infrastructure/"]
RUN dotnet restore "./AIJobCoaches.Worker/AIJobCoaches.Worker.csproj"
COPY . .
WORKDIR "/src/AIJobCoaches.Worker"
RUN dotnet build "./AIJobCoaches.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AIJobCoaches.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AIJobCoaches.Worker.dll"]