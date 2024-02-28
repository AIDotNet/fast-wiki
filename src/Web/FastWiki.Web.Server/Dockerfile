#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0.2-bookworm-slim-amd64 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Web/FastWiki.Web.Server/FastWiki.Web.Server.csproj", "src/Web/FastWiki.Web.Server/"]
COPY ["src/ApiGateway/FastWiki.ApiGateway.Caller/FastWiki.ApiGateway.Caller.csproj", "src/ApiGateway/FastWiki.ApiGateway.Caller/"]
COPY ["src/Contracts/FastWiki.Service.Contracts/FastWiki.Service.Contracts.csproj", "src/Contracts/FastWiki.Service.Contracts/"]
COPY ["src/Web/FastWiki.Web.Rcl/FastWiki.Web.Rcl.csproj", "src/Web/FastWiki.Web.Rcl/"]
COPY ["src/Infrastructure/FastWiki.Infrastructure.Common/FastWiki.Infrastructure.Common.csproj", "src/Infrastructure/FastWiki.Infrastructure.Common/"]
COPY ["src/Infrastructure/FastWiki.Infrastructure.Rcl.Command/FastWiki.Infrastructure.Rcl.Command.csproj", "src/Infrastructure/FastWiki.Infrastructure.Rcl.Command/"]
RUN dotnet restore "./src/Web/FastWiki.Web.Server/FastWiki.Web.Server.csproj"
COPY . .
WORKDIR "/src/src/Web/FastWiki.Web.Server"
RUN dotnet build "./FastWiki.Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FastWiki.Web.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FastWiki.Web.Server.dll"]