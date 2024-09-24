FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

COPY Fieldy.BookingYard.sln Fieldy.BookingYard.sln
COPY Api/Fieldy.BookingYard.Api/Fieldy.BookingYard.Api.csproj Api/Fieldy.BookingYard.Api/Fieldy.BookingYard.Api.csproj
COPY Core/Fieldy.BookingYard.Application/Fieldy.BookingYard.Application.csproj Core/Fieldy.BookingYard.Application/Fieldy.BookingYard.Application.csproj
COPY Core/Fieldy.BookingYard.Domain/Fieldy.BookingYard.Domain.csproj Core/Fieldy.BookingYard.Domain/Fieldy.BookingYard.Domain.csproj
COPY Infrastructure/Fieldy.BookingYard.Infrastructure/Fieldy.BookingYard.Infrastructure.csproj Infrastructure/Fieldy.BookingYard.Infrastructure/Fieldy.BookingYard.Infrastructure.csproj
COPY Infrastructure/Fieldy.BookingYard.Persistence/Fieldy.BookingYard.Persistence.csproj Infrastructure/Fieldy.BookingYard.Persistence/Fieldy.BookingYard.Persistence.csproj
# Restore package deps
RUN dotnet restore Fieldy.BookingYard.sln

COPY . .

# Build the project
WORKDIR /app/Api/Fieldy.BookingYard.Api
RUN dotnet publish -c Release -o /app/out

# build runtime  image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
RUN mkdir -p /app/wwwroot
VOLUME /app/uploads

COPY --from=build /app/out .

ENTRYPOINT [ "dotnet","Fieldy.BookingYard.Api.dll"]
