# Microservices.Demo.Report.API

Reporting microservice. It does not own a persistence domain; it composes information by calling Product and Policy through HTTP clients discovered with Eureka.

## Technology

ASP.NET Core/.NET 6, MediatR, RestEase, and Steeltoe Config Server/Eureka. The entry point is `Program.cs`; the code uses `Aplication`, `CQRS`, `Domain`, and `Infraestucture`.

## Endpoint

- `GET /api/report/policies`: generates the policy report.

The operation uses the `AgentLogin` header to preserve the authenticated agent context. Through the Gateway it requires a valid JWT.

## Dependencies

- Product API and Policy API through Eureka.
- Config Server `microservices.demo.configserver:8889`.
- Eureka `microservices.demo.discoveryserver:8761`.
- Logstash, Jaeger, and APM.

It registers as `Microservices.Demo.Report.API`. Compose can create multiple replicas.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-all.yml build microservices.demo.report.api
docker-compose -f ..\..\..\docker-compose-all.yml up -d microservices.demo.report.api
```

## Local execution

Requires the .NET 6 SDK and Product API, Policy API, Config Server, and Eureka to be available. Run from this directory:

```powershell
dotnet restore
dotnet run
```

Report API has no own database: it builds the report by querying Product and Policy through Eureka. Use the Gateway to preserve authentication and routing.
