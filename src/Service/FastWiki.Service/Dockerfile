#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0.2-bookworm-slim-amd64 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Service/FastWiki.Service/FastWiki.Service.csproj", "src/Service/FastWiki.Service/"]
COPY ["src/Contracts/FastWiki.Service.Contracts/FastWiki.Service.Contracts.csproj", "src/Contracts/FastWiki.Service.Contracts/"]
COPY ["src/Infrastructure/FastWiki.Infrastructure.Common/FastWiki.Infrastructure.Common.csproj", "src/Infrastructure/FastWiki.Infrastructure.Common/"]
RUN dotnet restore "./src/Service/FastWiki.Service/FastWiki.Service.csproj"
COPY . .
WORKDIR "/src/src/Service/FastWiki.Service"
RUN dotnet build "./FastWiki.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FastWiki.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false


FROM node as builder
WORKDIR /src
COPY ["web","."]
RUN npm i
RUN npm run build


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=builder /src/dist ./wwwroot
ENTRYPOINT ["dotnet", "FastWiki.Service.dll"]