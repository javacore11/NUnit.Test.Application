
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_ENVIRONMENT="Development"
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NUnit.Test.Application/NUnit.Test.Application.csproj", "NUnit.Test.Application/"]
RUN dotnet restore "NUnit.Test.Application/NUnit.Test.Application.csproj"
COPY . .
WORKDIR "/src/NUnit.Test.Application"
RUN dotnet build "NUnit.Test.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NUnit.Test.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NUnit.Test.Application.dll"]