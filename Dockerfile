FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY src/EP.Query.WebApi/Nuget.Config /
COPY ["src/EP.Query.WebApi/EP.Query.WebApi.csproj", "src/EP.Query.WebApi/"]
COPY ["src/EP.Query.Migrator/EP.Query.Migrator.csproj", "src/EP.Query.Migrator/"]
COPY ["src/EP.Query.EntityFrameworkCore/EP.Query.EntityFrameworkCore.csproj", "src/EP.Query.EntityFrameworkCore/"]
COPY ["src/EP.Query.Core/EP.Query.Core.csproj", "src/EP.Query.Core/"]
COPY ["src/EP.Query.Application/EP.Query.Application.csproj", "src/EP.Query.Application/"]
RUN dotnet restore "src/EP.Query.WebApi/EP.Query.WebApi.csproj" --configfile /Nuget.Config
RUN dotnet restore "src/EP.Query.Migrator/EP.Query.Migrator.csproj" --configfile /Nuget.Config
COPY . .
WORKDIR "/src/src/EP.Query.WebApi"
RUN dotnet build "EP.Query.WebApi.csproj" -c Release

FROM build AS publish
RUN dotnet publish "EP.Query.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENV ASPNETCORE_ENVIRONMENT "Production"
ENV ASPNETCORE_HOSTINGSTARTUPASSEMBLIES "SkyAPM.Agent.AspNetCore"
ENV SKYWALKING__SERVICENAME "EP.Query"
CMD [ "--App:ServerRootAddress=http://localhost:18031/ --App:SwaggerJsonAddress=http://localhost:18031/" ]
ENTRYPOINT ["dotnet", "EP.Query.WebApi.dll"]




