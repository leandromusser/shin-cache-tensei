FROM mcr.microsoft.com/dotnet/sdk:6.0 as base
WORKDIR /app
COPY src .
RUN dotnet publish "ShinCacheTensei.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /ShinCacheTensei
COPY --from=base /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ShinCacheTensei.dll