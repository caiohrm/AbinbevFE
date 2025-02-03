
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AbInbev/AbInbev.csproj", "src/AbInbev/"]
COPY ["Application/Application.csproj", "src/Application/"]
COPY ["CrossCutting/CrossCutting.csproj", "src/CrossCutting/"]
COPY ["Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]

RUN dotnet restore "./src/AbInbev/AbInbev.csproj"
COPY . .
WORKDIR "/src/AbInbev"
RUN dotnet build "./AbInbev.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AbInbev.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "AbInbev.dll"]