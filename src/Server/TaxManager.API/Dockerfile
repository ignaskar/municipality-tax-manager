#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["src/Server/TaxManager.API/TaxManager.API.csproj", "src/Server/TaxManager.API/"]
COPY ["src/Server/TaxManager.Infra/TaxManager.Infra.csproj", "src/Server/TaxManager.Infra/"]
COPY ["src/Server/TaxManager.Core/TaxManager.Core.csproj", "src/Server/TaxManager.Core/"]
RUN dotnet restore "src/Server/TaxManager.API/TaxManager.API.csproj"
COPY . .
WORKDIR "/src/src/Server/TaxManager.API"
RUN dotnet build "TaxManager.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaxManager.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaxManager.API.dll"]
