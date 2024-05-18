#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Catalog/Eshop.Catalog.Api/Eshop.Catalog.Api.csproj", "Services/Catalog/Eshop.Catalog.Api/"]
COPY ["Common/Eshop.Common.Web/Eshop.Common.Web.csproj", "Common/Eshop.Common.Web/"]
COPY ["Common/Eshop.Common.Functional/Eshop.Common.Functional.csproj", "Common/Eshop.Common.Functional/"]
COPY ["Common/Eshop.Common/Eshop.Common.csproj", "Common/Eshop.Common/"]
RUN dotnet restore "./Services/Catalog/Eshop.Catalog.Api/Eshop.Catalog.Api.csproj"
COPY . .
WORKDIR "/src/Services/Catalog/Eshop.Catalog.Api"
RUN dotnet build "./Eshop.Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Eshop.Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Eshop.Catalog.Api.dll"]