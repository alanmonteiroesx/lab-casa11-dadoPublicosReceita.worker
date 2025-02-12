FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY src/DadosPublicosReceita.Application/*.csproj ./src/DadosPublicosReceita.Application/
COPY src/DadosPublicosReceita.Domain/*.csproj ./src/DadosPublicosReceita.Domain/
COPY src/DadosPublicosReceita.Infrastructure/*.csproj ./src/DadosPublicosReceita.Infrastructure/
COPY src/DadosPublicosReceita.Shared/*.csproj ./src/DadosPublicosReceita.Shared/
COPY src/DadosPublicosReceita.Worker/*.csproj ./src/DadosPublicosReceita.Worker/

COPY *.sln .
RUN dotnet restore src/DadosPublicosReceita.Worker/DadosPublicosReceita.Worker.csproj

COPY src/. ./src/

RUN dotnet publish src/DadosPublicosReceita.Worker/DadosPublicosReceita.Worker.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS migrations
WORKDIR /app
COPY --from=build /app .

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["dotnet-ef", "database", "update", "--project", "src/DadosPublicosReceita.Infrastructure", "--startup-project", "src/DadosPublicosReceita.Worker"]

FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "DadosPublicosReceita.Worker.dll"]
