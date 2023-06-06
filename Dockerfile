FROM mcr.microsoft.com/dotnet/sdk:6.0 as base
WORKDIR /app
COPY src .
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef migrations add inicial
RUN dotnet ef database update

RUN dotnet publish "ShinCacheTensei.csproj" -c Release -o /app/publish
RUN mv shincachetensei.db /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /ShinCacheTensei
COPY --from=base /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT ASPNETCORE_ENVIRONMENT="Development" dotnet ShinCacheTensei.dll
