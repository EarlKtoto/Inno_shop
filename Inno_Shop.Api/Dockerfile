FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Inno_Shop.Api/Inno_Shop.Api.csproj", "Inno_Shop.Api/"]
COPY ["Inno_Shop.Application/Inno_Shop.Application.csproj", "Inno_Shop.Application/"]
COPY ["Inno_Shop.Infrastructure/Inno_Shop.Infrastructure.csproj", "Inno_Shop.Infrastructure/"]
COPY ["Inno_Shop.Domain/Inno_Shop.Domain.csproj", "Inno_Shop.Domain/"]
RUN dotnet restore "Inno_Shop.Api/Inno_Shop.Api.csproj"
COPY . .
WORKDIR "/src/Inno_Shop.Api"
RUN dotnet build "./Inno_Shop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Inno_Shop.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inno_Shop.Api.dll"]
