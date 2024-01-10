#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5156
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Library.ApiServer/Library.ApiServer.csproj", "Library.ApiServer/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Contracts.Mappers/Contracts.Mappers.csproj", "Contracts.Mappers/"]
COPY ["Contracts/Contracts.csproj", "Contracts/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Contracts.Validation/Contracts.Validation.csproj", "Contracts.Validation/"]
COPY ["Handlers/Handlers.csproj", "Handlers/"]
COPY ["Persistance/Persistance.csproj", "Persistance/"]

RUN dotnet restore "Library.ApiServer/Library.ApiServer.csproj"
COPY . .
WORKDIR "/src/Library.ApiServer"
RUN dotnet build "Library.ApiServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Library.ApiServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Library.ApiServer.dll"]