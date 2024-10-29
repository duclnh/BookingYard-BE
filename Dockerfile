FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY Api/Fieldy.BookingYard.Api/Fieldy.BookingYard.Api.csproj Api/Fieldy.BookingYard.Api/Fieldy.BookingYard.Api.csproj
COPY Core/Fieldy.BookingYard.Application/Fieldy.BookingYard.Application.csproj Core/Fieldy.BookingYard.Application/Fieldy.BookingYard.Application.csproj
COPY Core/Fieldy.BookingYard.Domain/Fieldy.BookingYard.Domain.csproj Core/Fieldy.BookingYard.Domain/Fieldy.BookingYard.Domain.csproj
COPY Infrastructure/Fieldy.BookingYard.Infrastructure/Fieldy.BookingYard.Infrastructure.csproj Infrastructure/Fieldy.BookingYard.Infrastructure/Fieldy.BookingYard.Infrastructure.csproj
COPY Infrastructure/Fieldy.BookingYard.Persistence/Fieldy.BookingYard.Persistence.csproj Infrastructure/Fieldy.BookingYard.Persistence/Fieldy.BookingYard.Persistence.csproj
# Restore package deps
RUN dotnet restore Api/Fieldy.BookingYard.Api/Fieldy.BookingYard.Api.csproj

# Copy the rest of the source code
COPY . .

# Build the project
WORKDIR "/src/Api/Fieldy.BookingYard.Api"
RUN dotnet build "Fieldy.BookingYard.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Fieldy.BookingYard.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
RUN mkdir -p /app/wwwroot
VOLUME /app/wwwroot
RUN apt-get update && apt-get install -y libgdiplus libc6-dev
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fieldy.BookingYard.Api.dll"]
