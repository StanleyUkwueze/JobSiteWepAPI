FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build-env
WORKDIR /app

COPY ./ ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:3.1
WORKDIR /app
EXPOSE 80

COPY --from=build-env /app/out .

ENV ASPNETCORE_URLS=http://*:$PORT
ENTRYPOINT ["dotnet", "WebsiteAPI.dll"]