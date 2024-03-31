#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserRepository.XUnit.Test/UserRepository.XUnit.Test.csproj", "UserRepository.XUnit.Test/"]
COPY ["NUnit.Test.Application/NUnit.Test.Application.csproj", "NUnit.Test.Application/"]
RUN dotnet restore "UserRepository.XUnit.Test/UserRepository.XUnit.Test.csproj"
COPY . .
WORKDIR "/src/UserRepository.XUnit.Test"
RUN dotnet build "UserRepository.XUnit.Test.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserRepository.XUnit.Test.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserRepository.XUnit.Test.dll"]