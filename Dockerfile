FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

RUN apt-get update -y
RUN apt-get install -y icu-devtools

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV TZ=Europe/Stockholm

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY src/BlazorApp.csproj .
RUN dotnet restore "BlazorApp.csproj"
ADD src .
RUN dotnet build "BlazorApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlazorApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet", "BlazorApp.dll"]