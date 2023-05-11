#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Core/Core.csproj", "Core/"]
COPY ["SBA.DataAccess/SBA.DataAccess.csproj", "SBA.DataAccess/"]
COPY ["SBA.Business/SBA.Business.csproj", "SBA.Business/"]
COPY ["SBA.MvcUI/SBA.MvcUI.csproj", "SBA.MvcUI/"]
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./SBA.MvcUI/*.csproj -c Release -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:7777"
ENTRYPOINT ["dotnet", "SBA.MvcUI.dll"]