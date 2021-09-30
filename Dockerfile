FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
LABEL stage=builder
WORKDIR /app

COPY . .
RUN dotnet restore

WORKDIR /app/Api
RUN dotnet publish -c release -o /out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=build /out ./
ENTRYPOINT ["dotnet", "Api.dll"]
