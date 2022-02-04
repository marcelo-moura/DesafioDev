#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DesafioDev.WebAPI/DesafioDev.WebAPI.csproj", "DesafioDev.WebAPI/"]
COPY ["DesafioDev.Application/DesafioDev.Application.csproj", "DesafioDev.Application/"]
COPY ["DesafioDev.Business/DesafioDev.Business.csproj", "DesafioDev.Business/"]
COPY ["DesafioDev.Core/DesafioDev.Core.csproj", "DesafioDev.Core/"]
COPY ["DesafioDev.Infra.Common/DesafioDev.Infra.Common.csproj", "DesafioDev.Infra.Common/"]
COPY ["DesafioDev.Infra.Globalization/DesafioDev.Infra.Globalization.csproj", "DesafioDev.Infra.Globalization/"]
COPY ["DesafioDev.Infra.IoC/DesafioDev.Infra.IoC.csproj", "DesafioDev.Infra.IoC/"]
COPY ["DesafioDev.Infra.Data/DesafioDev.Infra.Data.csproj", "DesafioDev.Infra.Data/"]
COPY ["DesafioDev.Infra.Integration/DesafioDev.Infra.Integration.csproj", "DesafioDev.Infra.Integration/"]
COPY ["DesafioDev.Infra.InterfacesRepository/DesafioDev.Infra.InterfacesRepository.csproj", "DesafioDev.Infra.InterfacesRepository/"]
RUN dotnet restore "DesafioDev.WebAPI/DesafioDev.WebAPI.csproj"
COPY . .
WORKDIR "/src/DesafioDev.WebAPI"
RUN dotnet build "DesafioDev.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DesafioDev.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DesafioDev.WebAPI.dll"]
