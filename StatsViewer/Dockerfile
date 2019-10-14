FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["../StatsViewer/StatsViewer.csproj", "../StatsViewer/"]
RUN dotnet restore "../StatsViewer/StatsViewer.csproj"
COPY . .
WORKDIR "/src/../StatsViewer"
RUN dotnet build "StatsViewer.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "StatsViewer.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "StatsViewer.dll"]