FROM mcr.microsoft.com/dotnet/sdk:9.0 as build
WORKDIR /app
EXPOSE 80

# copy all .csproj files and restore as distinct layers. Use of the same COPY command
# for every dockerfile in the project to take advantage of docker caching
COPY car-portal-sales.sln car-portal-sales.sln
COPY src/AuctionService/AuctionService.csproj src/AuctionService/AuctionService.csproj
COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
COPY src/BiddingService/BiddingService.csproj src/BiddingService/BiddingService.csproj
COPY src/NotificationService/NotificationService.csproj src/NotificationService/NotificationService.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj
COPY tests/AuctionService.UnitTests/AuctionService.UnitTests.csproj tests/AuctionService.UnitTests/AuctionService.UnitTests.csproj
COPY tests/AuctionService.IntegrationTests/AuctionService.IntegrationTests.csproj tests/AuctionService.IntegrationTests/AuctionService.IntegrationTests.csproj 
COPY tests/SearchService.IntegrationTests/SearchService.IntegrationTests.csproj tests/SearchService.IntegrationTests/SearchService.IntegrationTests.csproj

# Restore package deps
RUN dotnet restore car-portal-sales.sln

# copy the app folders over
COPY src/IdentityService src/IdentityService
WORKDIR /app/src/IdentityService
RUN dotnet publish -c Release -o /app/src/out


# build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT ["dotnet", "IdentityService.dll"]