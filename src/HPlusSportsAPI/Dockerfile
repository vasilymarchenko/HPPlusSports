#See https://docs.microsoft.com/en-us/visualstudio/containers/container-build to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#docker build -f Dockerfile -t demo-hplusspotsapi:v1 ..


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["HPlusSportsAPI/HPlusSportsAPI.csproj", "HPlusSportsAPI/"]
RUN dotnet restore "HPlusSportsAPI/HPlusSportsAPI.csproj"
COPY . .
WORKDIR "/src/HPlusSportsAPI"
RUN dotnet build "HPlusSportsAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HPlusSportsAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HPlusSportsAPI.dll"]