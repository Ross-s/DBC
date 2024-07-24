#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM node:20.12.2 AS node
WORKDIR /src
COPY . .
WORKDIR /src/DBC.Web/
RUN npm ci
RUN npm run build


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DBC.Web/DBC.Web.csproj", "DBC.Web/"]
RUN dotnet restore "./DBC.Web/DBC.Web.csproj"
COPY . .
WORKDIR "/src/DBC.Web"
COPY --from=node /src/DBC.Web/wwwroot/. ./wwwroot
RUN dotnet build "./DBC.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DBC.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DBC.Web.dll"]
