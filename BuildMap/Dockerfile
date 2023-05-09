#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
EXPOSE 8080/tcp

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BuildMap/BuildMap.csproj", "BuildMap/"]
RUN dotnet restore "BuildMap/BuildMap.csproj"
COPY . .
WORKDIR "/src/BuildMap"
RUN dotnet build "BuildMap.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BuildMap.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuildMap.dll"]