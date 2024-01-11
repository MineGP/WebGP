#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /
RUN  apt-get update \
  && apt-get install -y wget \
  && rm -rf /var/lib/apt/lists/* \
  && wget --no-verbose -c https://download.oracle.com/graalvm/17/latest/graalvm-jdk-17_linux-x64_bin.tar.gz -O - | tar -xz \
  && mv /graalvm-jdk-17.0.8+9.1/ /jdk/
ENV PATH="${PATH}:/jdk/bin"
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WebGP/WebGP.csproj", "src/WebGP/"]
COPY ["src/WebGP.Application/WebGP.Application.csproj", "src/WebGP.Application/"]
COPY ["src/WebGP.Domain/WebGP.Domain.csproj", "src/WebGP.Domain/"]
COPY ["src/WebGP.Diaka/WebGP.Diaka.csproj", "src/WebGP.Diaka/"]
COPY ["src/WebGP.Infrastructure/WebGP.Infrastructure.csproj", "src/WebGP.Infrastructure/"]
COPY ["src/WebGP.Java/WebGP.Java.csproj", "src/WebGP.Java/"]
RUN dotnet restore "src/WebGP/WebGP.csproj"
COPY . .
WORKDIR "/src/src/WebGP"
RUN dotnet build "WebGP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebGP.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebGP.dll"]