#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HybridCacheApi/HybridCacheApi.csproj", "HybridCacheApi/"]
RUN dotnet restore "HybridCacheApi/HybridCacheApi.csproj"
COPY . .
WORKDIR "/src/HybridCacheApi"
RUN dotnet build "HybridCacheApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HybridCacheApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HybridCacheApi.dll"]