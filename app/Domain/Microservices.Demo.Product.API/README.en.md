# Microservices.Demo.Product.API

Product catalog microservice. It exposes product information and lifecycle operations consumed by Policy, Report, and the web client.

## Technology

ASP.NET Core/.NET 6, EF Core, SQL Server, MediatR, and Steeltoe for Config Server/Eureka. The entry point is `Program.cs`; controllers are under `Controllers`, and the logic is separated into `Application`, `CQRS`, `Domain`, and `Infrastructure`.

## Endpoints

The base route is `/api/products`:

- `GET /api/products`: lists products.
- `GET /api/products/{code}`: gets a product by code.
- `POST /api/products`: creates a product.
- `POST /activate`: activates a product.
- `POST /discontinue`: discontinues a product.

Protected routes require the JWT issued by Auth API when accessed through the Gateway.

## Dependencies

- SQL Server `microservices.demo.product.db`, local port `14331`.
- Config Server `microservices.demo.configserver:8889`.
- Eureka `microservices.demo.discoveryserver:8761`.
- Logstash, Jaeger, and APM for observability.

The service registers in Eureka as `Microservices.Demo.Product.API`. Compose can start multiple replicas; their internal port is `80`, so access is recommended through the Gateway.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-all.yml build microservices.demo.product.api
docker-compose -f ..\..\..\docker-compose-all.yml up -d microservices.demo.product.api
```

The database must be available before starting the API.

## Local execution

Requires the .NET 6 SDK. Start Product DB, Config Server, and Eureka first, then run from this directory:

```powershell
dotnet restore
dotnet run
```

The API uses external configuration to connect to SQL Server and register with Eureka. For a complete test, access its routes through the Gateway at `http://localhost:44399`.
