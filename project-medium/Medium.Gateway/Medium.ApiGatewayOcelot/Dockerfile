#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Medium.ApiGatewayOcelot/Medium.ApiGatewayOcelot.csproj", "Medium.ApiGatewayOcelot/"]
RUN dotnet restore "Medium.ApiGatewayOcelot/Medium.ApiGatewayOcelot.csproj"
COPY . .
WORKDIR "/src/Medium.ApiGatewayOcelot"
RUN dotnet build "Medium.ApiGatewayOcelot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Medium.ApiGatewayOcelot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medium.ApiGatewayOcelot.dll"]