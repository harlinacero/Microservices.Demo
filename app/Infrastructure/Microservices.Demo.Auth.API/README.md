# Microservices.Demo.Auth.API

Servicio de autenticación y usuarios. Lee usuarios desde MongoDB, valida credenciales y emite tokens JWT para el resto de la plataforma.

## Tecnología

ASP.NET Core/.NET 6, MongoDB.Driver, JWT y Steeltoe Config Server/Eureka. La entrada es `Program.cs`; el controlador principal es `Controllers/UsersController.cs`.

## Endpoints

- `POST /api/users`: permite autenticación anónima y devuelve un JWT.
- `GET /api/users`: devuelve el usuario del token; requiere autenticación.

Usuarios iniciales de demo definidos en `db/Microservices.Demo.Auth.DB/Docker/init.js`: `erick/erick`, `eva/eva` y `oscar/oscar`.

## Dependencias

- MongoDB `microservices.demo.auth.db`, puerto local `27018`; base `SecurityDB`, colección `Users`.
- Config Server `microservices.demo.configserver:8889`.
- Eureka `microservices.demo.discoveryserver:8761`.
- Logstash, Jaeger y APM.

Se registra como `Microservices.Demo.Auth.API` y escucha internamente en el puerto `80`.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml build microservices.demo.auth.api
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.auth.api
```

Las credenciales incluidas son solo para la demo y no deben reutilizarse.
