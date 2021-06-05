#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Medium.Accounts.APIRest/Medium.Accounts.APIRest.csproj", "Medium.Accounts.APIRest/"]
RUN dotnet restore "Medium.Accounts.APIRest/Medium.Accounts.APIRest.csproj"
COPY . .
WORKDIR "/src/Medium.Accounts.APIRest"
RUN dotnet build "Medium.Accounts.APIRest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Medium.Accounts.APIRest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medium.Accounts.APIRest.dll"]