# Microservices.Demo.Pricing.API

Microservicio responsable de precios. Mantiene las tarifas en PostgreSQL y expone una operación que Policy utiliza para calcular el precio de una póliza.

## Tecnología

ASP.NET Core/.NET 6, Marten sobre PostgreSQL, MediatR y Steeltoe Config Server/Eureka. La entrada es `Program.cs`; la lógica está organizada en `Application`, `CQRS`, `Domain` e `Infrastructure`.

## Endpoint

- `POST /api/pricing`: calcula o consulta el precio a partir de los datos enviados por el consumidor.

La ruta pública se obtiene mediante el Gateway; la llamada interna de Policy usa descubrimiento Eureka.

## Dependencias

- PostgreSQL `microservices.demo.pricing.db`, puerto local `54321`.
- Config Server `microservices.demo.configserver:8889`.
- Eureka `microservices.demo.discoveryserver:8761`.
- Logstash, Jaeger y APM.

La base crea la base `Microservices.Demo.Pricing` al inicializarse. El API se registra en Eureka como `Microservices.Demo.Pricing.API`.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-all.yml build microservices.demo.pricing.api
docker-compose -f ..\..\..\docker-compose-all.yml up -d microservices.demo.pricing.api
```

Inicia PostgreSQL con `docker-compose-db.yml` antes del servicio.

## Ejecución local

Requiere el SDK de .NET 6. Con PostgreSQL, Config Server y Eureka disponibles, ejecuta desde esta carpeta:

```powershell
dotnet restore
dotnet run
```

Pricing API cargará su conexión a PostgreSQL desde Config Server y se registrará en Eureka. Policy normalmente lo consume por descubrimiento; para el flujo público utiliza el Gateway.
