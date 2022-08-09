#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["eSports Results API/eSportsResults.API.csproj", "eSports Results API/"]
RUN dotnet restore "eSports Results API/eSportsResults.API.csproj"
COPY . .
WORKDIR "/src/eSports Results API"
RUN dotnet build "eSportsResults.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eSportsResults.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eSportsResults.API.dll"]