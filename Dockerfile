# -----------------------
# 1. Build stage
# -----------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project file and restore dependencies
COPY ApiPoject2.csproj ./
RUN dotnet restore

# Copy all source files and publish
COPY . ./
RUN dotnet publish -c Release -o out

# -----------------------
# 2. Runtime stage
# -----------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/out ./

# Expose default port
EXPOSE 80

# Run the app
ENTRYPOINT ["dotnet", "ApiPoject2.dll"]
