# Microservices.Demo.Client.Web.ApiGateway

The API Gateway is the single entry point for the web client. It centralizes HTTP access, prevents internal APIs from being exposed, validates JWTs, and forwards each request to the correct microservice. It is built with ASP.NET Core/.NET 6, Ocelot, and the Eureka provider.

## Configuration flow

1. `Program.cs` calls `AddConfigServer(...)`.
2. Steeltoe discovers Config Server through Eureka.
3. Config Server reads `Microservices.Demo-config` from Git.
4. Ocelot receives the routes and uses Eureka to locate each `ServiceName`.

The external repository must contain `Microservices.Demo.Client.Web.ApiGateway/application.yml` on the `main` branch.

## What it solves

- Hides internal microservice addresses and ports.
- Provides a stable public URL (`44399`).
- Applies authentication and claim requirements per route.
- Uses service discovery to locate dynamic instances.
- Allows routes to change without recompiling the Gateway.

## Main routes

External configuration defines `/api/users`, `/api/products`, `/api/offers`, `/api/policies`, and `/api/report/policies`. Auth uses an anonymous POST for login; protected requests use JWTs and may require `SALESMAN` claims.

## Request flow

```text
Client -> Gateway -> Ocelot configuration -> Eureka -> destination API -> database
```

The Gateway does not execute product or policy business logic. A `401` usually means authentication failed; a `502` or `503` usually points to Eureka or the destination service. If the socket closes during startup, check Config Server.

## Ports and execution

The container publishes `44399:80`:

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml build microservices.demo.client.web.apigateway
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.client.web.apigateway
```

## Diagnostics

If the socket closes or the container restarts, check:

```powershell
docker logs Microservices.Demo.ConfigServer
docker logs Microservices.Demo.Client.Web.ApiGateway
curl.exe -i http://localhost:8889/Microservices.Demo.Client.Web.ApiGateway/Production
```

`failFast=true` makes the Gateway stop if Config Server cannot load the repository or the configured branch does not exist.
