# Microservices.Demo.Pricing.API

Pricing microservice. It stores pricing rules in PostgreSQL and exposes the operation Policy uses to calculate a policy price.

## Technology

ASP.NET Core/.NET 6, Marten on PostgreSQL, MediatR, and Steeltoe Config Server/Eureka. The entry point is `Program.cs`; logic is organized into `Application`, `CQRS`, `Domain`, and `Infrastructure`.

## Endpoint

- `POST /api/pricing`: calculates or retrieves a price from the data submitted by the consumer.

The public route is obtained through the Gateway; Policy's internal call uses Eureka service discovery.

## Dependencies

- PostgreSQL `microservices.demo.pricing.db`, local port `54321`.
- Config Server `microservices.demo.configserver:8889`.
- Eureka `microservices.demo.discoveryserver:8761`.
- Logstash, Jaeger, and APM.

The database `Microservices.Demo.Pricing` is created during initialization. The API registers in Eureka as `Microservices.Demo.Pricing.API`.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-all.yml build microservices.demo.pricing.api
docker-compose -f ..\..\..\docker-compose-all.yml up -d microservices.demo.pricing.api
```

Start PostgreSQL with `docker-compose-db.yml` before the service.

## Local execution

Requires the .NET 6 SDK. With PostgreSQL, Config Server, and Eureka available, run from this directory:

```powershell
dotnet restore
dotnet run
```

Pricing API loads its PostgreSQL connection from Config Server and registers with Eureka. Policy normally consumes it through discovery; use the Gateway for the public flow.
