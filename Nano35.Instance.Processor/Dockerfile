FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /src
RUN git clone https://github.com/CandyChips/Nano35.Instance ./Nano35.Instance.Processor/*
RUN dotnet restore Nano35.Instance.Processor.csproj --configfile ./NuGet.config
WORKDIR /src/.
RUN dotnet build Nano35.Instance.Processor.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Nano35.Instance.Processor.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Nano35.Instance.Processor.dll"]