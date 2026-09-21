# Microservices.Demo.ConfigServer

Centralized configuration server based on Spring Cloud Config Server. It avoids duplicating configuration inside each image and allows .NET clients to obtain properties from a shared Git repository.

## What it manages

- Application identity and environment.
- Eureka URL and settings.
- Connection strings and database names.
- Security configuration.
- Ocelot Gateway routes.

Config Server is neither a database nor a business proxy: it reads configuration files from Git and exposes them through an HTTP API.

## Configuration

Configuration is in `src/main/resources/application.yml`:

- Port: `8889`.
- Repository: `https://github.com/harlinacero/Microservices.Demo-config.git`.
- Branch: `main`.
- Per-application folder: `{application}`.
- Eureka registration: `microservices.demo.discoveryserver:8761`.

The repository must contain folders with the exact application name, for example `Microservices.Demo.Client.Web.ApiGateway/application.yml`.

## Query flow

1. A client registers or discovers itself through Eureka.
2. Steeltoe requests `/application/profile/label` from Config Server.
3. Config Server clones or updates the Git repository.
4. It finds the application folder and combines its YAML/properties files.
5. It returns the properties to the client.

With `failFast=true`, a URL, branch, network, or folder-name error prevents the client from finishing startup.

## Useful queries

```powershell
curl.exe http://localhost:8889/Microservices.Demo.Client.Web.ApiGateway/Production
curl.exe http://localhost:8889/Microservices.Demo.Auth.API/Production
```

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml build microservices.demo.configserver
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.configserver
```

The Dockerfile uses Maven/Java 17 and downloads the OpenTelemetry agent during the build. If a client reports `Could not locate PropertySource`, first check the URL, branch, folder name, and this container's logs.
