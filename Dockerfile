FROM microsoft/dotnet:2.2-sdk-alpine AS build
# Set the working directory witin the container
WORKDIR /build

# Copy the sln and csproj files. These are the only files
# required in order to restore
COPY ./applevalApi.Common/applevalApi.Common.csproj ./applevalApi.Common/applevalApi.Common.csproj
COPY ./applevalApi.DAL/applevalApi.DAL.csproj ./applevalApi.DAL/applevalApi.DAL.csproj
COPY ./applevalApi.DTO/applevalApi.DTO.csproj ./applevalApi.DTO/applevalApi.DTO.csproj
COPY ./applevalApi.Entities/applevalApi.Entities.csproj ./applevalApi.Entities/applevalApi.Entities.csproj
COPY ./applevalApi.Persistence/applevalApi.Persistence.csproj ./applevalApi.Persistence/applevalApi.Persistence.csproj
COPY ./applevalApi.Tests/applevalApi.Tests.csproj ./applevalApi.Tests/applevalApi.Tests.csproj
COPY ./applevalApi/applevalApi.csproj ./applevalApi/applevalApi.csproj
# COPY ./applevalApi.sln ./applevalApi.sln
COPY ./global.json ./global.json

# Restore all packages
RUN dotnet restore ./applevalApi/applevalApi.csproj --force --no-cache

# Copy the remaining source
COPY ./applevalApi.Common/ ./applevalApi.Common/
COPY ./applevalApi.DAL/ ./applevalApi.DAL/
COPY ./applevalApi.DTO/ ./applevalApi.DTO/
COPY ./applevalApi.Entities/ ./applevalApi.Entities/
COPY ./applevalApi.Persistence/ ./applevalApi.Persistence/
COPY ./applevalApi.Tests/ ./applevalApi.Tests/
COPY ./applevalApi/ ./applevalApi/

# Build the source code
RUN dotnet build ./applevalApi/applevalApi.csproj --configuration Release --no-restore

# Ensure that we generate and migrate the database 
WORKDIR ./applevalApi.Persistence
RUN dotnet ef database update

# # Run all tests
# WORKDIR ./applevalApi.Tests
# RUN dotnet xunit

# Publish application
WORKDIR ..
RUN dotnet publish ./applevalApi/applevalApi.csproj --configuration Release --no-restore --no-build --output "../dist"

# Copy the created database
RUN cp ./applevalApi.Persistence/dwDatabase.db ./dist/dwDatabase.db

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS app
WORKDIR /app
COPY --from=build /build/dist .
ENV ASPNETCORE_URLS http://+:5000

ENTRYPOINT ["dotnet", "applevalApi.dll"]