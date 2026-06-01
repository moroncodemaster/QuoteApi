FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG DOTNET_NUGET_SIGNATURE_VERIFICATION=false
ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=$DOTNET_NUGET_SIGNATURE_VERIFICATION
WORKDIR /src
COPY ["QuoteApi/QuoteApi.csproj", "./QuoteApiBuild/"]
RUN dotnet restore "QuoteApiBuild/QuoteApi.csproj"
COPY . .
WORKDIR "/src/QuoteApiBuild"
RUN dotnet build "./QuoteApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuoteApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY QuoteApi/.env .
ENTRYPOINT ["dotnet", "QuoteApi.dll"]
