# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /src

# Copy nuget.config file to the container
COPY NuGet.config ./

# Copy the csproj file and restore dependencies
COPY ["Vroom_Project/Vroom_Project.csproj", "Vroom_Project/"]
RUN dotnet restore "./Vroom_Project/Vroom_Project.csproj"

# Copy the entire project and build
COPY . .
WORKDIR "/src/Vroom_Project"
RUN dotnet build "Vroom_Project.csproj" -c Release -o /app/build

# Publish the project
RUN dotnet publish "Vroom_Project.csproj" -c Release -o /app/publish

# Stage 2: Serve
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose the port the app runs on
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "Vroom_Project.dll"]
