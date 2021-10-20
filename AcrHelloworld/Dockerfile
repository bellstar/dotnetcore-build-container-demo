#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
# Update <acrName> with the name of your registry
# Example: uniqueregistryname.azurecr.io
ENV DOCKER_REGISTRY acrhandsonhimiyaza.azurecr.io
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["AcrHelloworld/AcrHelloworld.csproj", "AcrHelloworld/"]
RUN dotnet restore "AcrHelloworld/AcrHelloworld.csproj"
COPY . .
WORKDIR "/src/AcrHelloworld"
RUN dotnet build "AcrHelloworld.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AcrHelloworld.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcrHelloworld.dll"]