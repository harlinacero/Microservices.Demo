# Microservices.Demo.DiscoveryServer

Spring Cloud Eureka server. It acts as a dynamic service directory: each API publishes its name and address at startup, and consumers ask Eureka where to find it.

## What it solves

- Avoids hard-coding container IP addresses that may change.
- Supports multiple instances of the same API.
- Helps the Gateway and internal clients select a destination.
- Shows the registration status of services.

## Configuration

- Port: `8761`.
- Name: `Microservices.Demo.DiscoveryServer`.
- Java entry point: `src/main/java/io/microservices/discoveryserver/DiscoveryServerApplication.java`.

.NET services register with names such as `Microservices.Demo.Auth.API`, `Microservices.Demo.Product.API`, and `Microservices.Demo.Policy.API`. The Gateway queries this registry through the Ocelot Eureka provider.

## Discovery flow

```text
API starts -> registers with Eureka -> consumer queries the name -> Eureka returns an instance
```

Eureka does not route or process business requests. It only maintains the registry; the Gateway or HTTP client makes the subsequent call.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml build microservices.demo.discoveryserver
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.discoveryserver
```

The console is available at `http://localhost:8761`. If a service does not appear, check its `appsettings.json`, Eureka name, and service logs.

A temporary absence during startup can be normal. If it persists, confirm that the service uses the same name as the Ocelot route `ServiceName` and points to `http://microservices.demo.discoveryserver:8761/eureka`.
