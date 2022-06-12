FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
LABEL stage=builder
WORKDIR /app

COPY . .
RUN dotnet restore

WORKDIR /app/Api
RUN dotnet publish -c release -o /out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
RUN apt-get update \
    && apt-get install -y --no-install-recommends libgdiplus libc6-dev \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "Api.dll"]
