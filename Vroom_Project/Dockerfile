# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

# Set working directory
WORKDIR /src

# Copy NuGet configuration file
COPY NuGet.config ./

# Clear NuGet cache
RUN dotnet nuget locals all --clear

# Copy the project file and restore as distinct layers
COPY ./Vroom_Project/Vroom_Project.csproj ./Vroom_Project/
WORKDIR /src/Vroom_Project

# Restore dependencies
RUN dotnet restore "./Vroom_Project.csproj" --disable-parallel

# Copy the rest of the application code
COPY . ./

# Build the project
RUN dotnet build "Vroom_Project.csproj" -c Release -o /app/build

# Publish the project
RUN dotnet publish "Vroom_Project.csproj" -c Release -o /app/publish

# Use the official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

# Copy the published files from the build image
COPY --from=build-env /app/publish .

# Expose the application port
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", "Vroom_Project.dll"]
