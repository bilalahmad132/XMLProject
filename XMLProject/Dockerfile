# Use the official ASP.NET Core runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore any dependencies (via dotnet restore)
COPY ["XMLProject.csproj", "./"]
RUN dotnet restore "./XMLProject.csproj"

# Copy the rest of the code
COPY . . 

# Publish the app to /app/publish directory in the container
WORKDIR "/src/"
RUN dotnet publish "XMLProject.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /app/publish . 

# Set the entry point to the application
ENTRYPOINT ["dotnet", "XMLProject.dll"]
