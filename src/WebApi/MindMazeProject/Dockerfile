#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WebApi/MindMazeProject/MindMazeProject.csproj", "src/WebApi/MindMazeProject/"]
COPY ["src/Core/Application/MindMaze.Core.Application/MindMaze.Core.Application.csproj", "src/Core/Application/MindMaze.Core.Application/"]
COPY ["src/Core/Domain/MindMaze.Core.Domain/MindMaze.Core.Domain.csproj", "src/Core/Domain/MindMaze.Core.Domain/"]
COPY ["src/Infrastructure/MindMaze.Infrastructure.infrastructure/MindMaze.Infrastructure.infrastructure.csproj", "src/Infrastructure/MindMaze.Infrastructure.infrastructure/"]
RUN dotnet restore "src/WebApi/MindMazeProject/MindMazeProject.csproj"
COPY . .
WORKDIR "/src/src/WebApi/MindMazeProject"
RUN dotnet build "MindMazeProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MindMazeProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MindMazeProject.dll"]