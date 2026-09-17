# Microservices.Demo.Report.API

Microservicio de reportes. No es dueño de un dominio de persistencia propio: compone información consultando Product y Policy mediante clientes HTTP descubiertos por Eureka.

## Tecnología

ASP.NET Core/.NET 6, MediatR, RestEase y Steeltoe Config Server/Eureka. La entrada es `Program.cs`; el código usa `Aplication`, `CQRS`, `Domain` e `Infraestucture`.

## Endpoint

- `GET /api/report/policies`: genera el reporte de pólizas.

La operación utiliza el header `AgentLogin` para conservar el contexto del agente autenticado. Mediante Gateway requiere un JWT válido.

## Dependencias

- Product API y Policy API mediante Eureka.
- Config Server `microservices.demo.configserver:8889`.
- Eureka `microservices.demo.discoveryserver:8761`.
- Logstash, Jaeger y APM.

Se registra como `Microservices.Demo.Report.API`. El Compose puede crear varias réplicas.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-all.yml build microservices.demo.report.api
docker-compose -f ..\..\..\docker-compose-all.yml up -d microservices.demo.report.api
```
