FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/Services/Audit/Audit.Application/Audit.Application.csproj", "src/Services/Audit/Audit.Application/"]
COPY ["src/Services/Audit/Audit.Infrastructure/Audit.Infrastructure.csproj", "src/Services/Audit/Audit.Infrastructure/"]
COPY ["src/Services/Audit/Audit.Domain/Audit.Domain.csproj", "src/Services/Audit/Audit.Domain/"]
RUN dotnet restore "src/Services/Audit/Audit.Application/Audit.Application.csproj"
COPY . .
WORKDIR "/src/src/Services/Audit/Audit.Application"
RUN dotnet build "Audit.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Audit.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Audit.Application.dll"]