FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QuoteApi/QuoteApi.csproj", "QuoteApi/"]
RUN dotnet restore "QuoteApi/QuoteApi.csproj"
COPY . .
WORKDIR "/src/QuoteApi"
RUN dotnet build "./QuoteApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuoteApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .




ENTRYPOINT ["dotnet", "QuoteApi.dll"]
