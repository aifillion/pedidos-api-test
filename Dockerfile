FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
# EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Pedidos.API/Pedidos.API.csproj", "Pedidos.API/"]
COPY ["Pedidos.API.Application/Pedidos.API.Application.csproj", "Pedidos.API.Application/"]
COPY ["Pedidos.API.Domain/Pedidos.API.Domain.csproj", "Pedidos.API.Domain/"]
COPY ["Pedidos.API.Infra/Pedidos.API.Infra.csproj", "Pedidos.API.Infra/"]
COPY ["Pedidos.API.Tests/Pedidos.API.Tests.csproj", "Pedidos.API.Tests/"]
RUN dotnet restore "Pedidos.API/Pedidos.API.csproj"
COPY . .
WORKDIR "/src/Pedidos.API"
RUN dotnet build "Pedidos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pedidos.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pedidos.API.dll"]