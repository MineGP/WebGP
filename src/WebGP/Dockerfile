#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WebGP/WebGP.csproj", "WebGP/"]
COPY ["src/WebGP.Application/WebGP.Application.csproj", "WebGP.Application/"]
COPY ["src/WebGP.Domain/WebGP.Domain.csproj", "WebGP.Domain/"]
COPY ["src/WebGP.Infrastructure/WebGP.Infrastructure.csproj", "WebGP.Infrastructure/"]
COPY ["src/WebGP.Diaka/WebGP.Diaka.csproj", "WebGP.Diaka/"]
RUN dotnet restore "WebGP/WebGP.csproj"
COPY . .
WORKDIR "/src/WebGP"
RUN dotnet build "WebGP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebGP.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebGP.dll"]