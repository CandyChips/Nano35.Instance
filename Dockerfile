FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
COPY . .
RUN dotnet restore Nano35.Instance.Api/Nano35.Instance.Api.csproj --configfile ./Nano35.Instance.Api/NuGet.config
WORKDIR /src/.
RUN dotnet build Nano35.Instance.Api/Nano35.Instance.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Nano35.Instance.Api/Nano35.Instance.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nano35.Instance.Api.dll"]