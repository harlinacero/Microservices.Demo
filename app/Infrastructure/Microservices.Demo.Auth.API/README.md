# Microservices.Demo.Auth.API

Servicio de identidad de la demo. Es el punto donde el usuario presenta sus credenciales; Auth API las valida contra MongoDB y devuelve un JWT que los demás servicios usan para autorizar operaciones.

## Tecnología

ASP.NET Core/.NET 6, MongoDB.Driver, JWT y Steeltoe Config Server/Eureka. La entrada es `Program.cs`; el controlador principal es `Controllers/UsersController.cs`.

## Endpoints

- `POST /api/users`: permite autenticación anónima y devuelve un JWT.
- `GET /api/users`: devuelve el usuario del token; requiere autenticación.

Usuarios iniciales de demo definidos en `db/Microservices.Demo.Auth.DB/Docker/init.js`: `erick/erick`, `eva/eva` y `oscar/oscar`.

## Flujo de autenticación

1. El cliente envía `POST /api/users` con usuario y contraseña.
2. El servicio busca el usuario en la colección `SecurityDB.Users`.
3. Si las credenciales son válidas, genera un JWT con la identidad y claims del usuario.
4. El cliente envía el token al Gateway con `Authorization: Bearer <token>`.
5. Gateway y APIs validan el token antes de permitir las operaciones protegidas.

El endpoint `GET /api/users` no es el login: devuelve el usuario asociado al token actual. Un `401` en ese endpoint significa que falta el token o que no es válido.

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

## Ejecución local

Requiere el SDK de .NET 6 y MongoDB disponible en `localhost:27018`. Con Config Server y Eureka levantados, ejecuta desde esta carpeta:

```powershell
dotnet restore
dotnet run
```

Para un entorno reproducible, ejecuta Auth API mediante `docker-compose-infr.yml`, que también conecta el servicio a la red `backend` y a su base MongoDB.
