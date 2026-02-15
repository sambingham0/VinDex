FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["VinDex.Api.csproj", "./"]
RUN dotnet restore "VinDex.Api.csproj"
COPY . .
RUN dotnet publish "VinDex.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "VinDex.Api.dll"]
