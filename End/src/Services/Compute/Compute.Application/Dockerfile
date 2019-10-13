FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/Compute/Compute.Application/Compute.Application.csproj", "src/Services/Compute/Compute.Application/"]
COPY ["src/Services/Compute/Compute.Infrastructure/Compute.Infrastructure.csproj", "src/Services/Compute/Compute.Infrastructure/"]
COPY ["src/Services/Compute/Compute.Domain/Compute.Domain.csproj", "src/Services/Compute/Compute.Domain/"]
RUN dotnet restore "src/Services/Compute/Compute.Application/Compute.Application.csproj"
COPY . .
WORKDIR "/src/src/Services/Compute/Compute.Application"
RUN dotnet build "Compute.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Compute.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Compute.Application.dll"]