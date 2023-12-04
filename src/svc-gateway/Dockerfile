FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80

# Copy all csproj files and resotre
COPY nextjs_with_dotnet.sln nextjs_with_dotnet.sln

COPY src/svc-auction/svc-auction.csproj src/svc-auction/svc-auction.csproj
COPY src/svc-search/svc-search.csproj src/svc-search/svc-search.csproj
COPY src/svc-gateway/svc-gateway.csproj src/svc-gateway/svc-gateway.csproj
COPY src/svc-identity/svc-identity.csproj src/svc-identity/svc-identity.csproj
COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj

# Restore package dependencies
RUN dotnet restore nextjs_with_dotnet.sln

# Copy the app folder over
COPY src/svc-gateway src/svc-gateway

WORKDIR /app/src/svc-gateway
RUN dotnet publish -c Release -o /app/src/out

# build runtimes image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet", "svc-gateway.dll" ]