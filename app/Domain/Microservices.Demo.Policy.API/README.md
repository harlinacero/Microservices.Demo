# Microservices.Demo.Policy.API

Microservicio de pólizas y ofertas. Coordina persistencia de pólizas, consulta de precios y mensajería de negocio.

## Tecnología

ASP.NET Core/.NET 6, EF Core y SQL Server, MediatR, RawRabbit/RabbitMQ, RestEase y Steeltoe Config Server/Eureka. La entrada es `Program.cs`; el código está separado en `Application`, `CQRS`, `Domain` e `Infrastructure`.

## Endpoints

- `POST /api/policies`: crea una póliza.
- `GET /api/policies`: lista pólizas.
- `GET /api/policies/{policyNumber}`: obtiene una póliza.
- `DELETE /terminate`: termina una póliza.
- `POST /api/offers`: crea/procesa una oferta.

Las operaciones protegidas requieren JWT. Offers usa el header `AgentLogin`, que el Gateway construye desde las claims del token.

## Dependencias

- SQL Server `microservices.demo.policy.db`, puerto local `14332`.
- Pricing API mediante Eureka.
- RabbitMQ `microservices.demo.rabbitmq`; management en `15672` y AMQP interno en `5672`.
- Config Server, Eureka, Logstash, Jaeger y APM.

El servicio se registra como `Microservices.Demo.Policy.API`.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-all.yml build microservices.demo.policy.api
docker-compose -f ..\..\..\docker-compose-all.yml up -d microservices.demo.policy.api
```
