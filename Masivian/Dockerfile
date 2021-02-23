FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Masivian/Masivian.csproj", "Masivian/"]
RUN dotnet restore "Masivian/Masivian.csproj"
COPY . .
WORKDIR "/src/Masivian"
RUN dotnet build "Masivian.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Masivian.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Masivian.dll"]