#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Medium.ApiGatewayOcelotJWT/Medium.ApiGatewayOcelotJWT.csproj", "Medium.ApiGatewayOcelotJWT/"]
RUN dotnet restore "Medium.ApiGatewayOcelotJWT/Medium.ApiGatewayOcelotJWT.csproj"
COPY . .
WORKDIR "/src/Medium.ApiGatewayOcelotJWT"
RUN dotnet build "Medium.ApiGatewayOcelotJWT.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Medium.ApiGatewayOcelotJWT.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medium.ApiGatewayOcelotJWT.dll"]