#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build


ARG BUILD_CONFIGURATION=Release
ARG BUILD_VERSION=0.0.0-LOCAL

ARG NUGET_REPOSITORY_URL
ARG NUGET_USERNAME
ARG NUGET_PASSWORD

WORKDIR /build

COPY . .

RUN dotnet nuget add source $NUGET_REPOSITORY_URL --name gitlab --username $NUGET_USERNAME --password $NUGET_PASSWORD --store-password-in-clear-text
RUN dotnet restore src/UtgService.sln --runtime linux-x64 --force --no-cache
RUN dotnet publish src/Utg.Api/Utg.Api.csproj -c $BUILD_CONFIGURATION -o /publish --runtime linux-x64 --self-contained false --no-restore /p:Version=$BUILD_VERSION /p:PublishReadyToRun=true
#

FROM mcr.microsoft.com/dotnet/aspnet:5.0.5-focal-amd64
WORKDIR /app
COPY --from=build /publish .
ENV ALLOWED_HOSTS "*"
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "Utg.Api.dll"]