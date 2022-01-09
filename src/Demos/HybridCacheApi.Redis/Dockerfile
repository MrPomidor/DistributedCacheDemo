#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Demos/HybridCacheApi.Redis/HybridCacheApi.Redis.csproj", "Demos/HybridCacheApi.Redis/"]
RUN dotnet restore "Demos/HybridCacheApi.Redis/HybridCacheApi.Redis.csproj"
COPY . .
WORKDIR "/src/Demos/HybridCacheApi.Redis"
RUN dotnet build "HybridCacheApi.Redis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HybridCacheApi.Redis.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HybridCacheApi.Redis.dll"]