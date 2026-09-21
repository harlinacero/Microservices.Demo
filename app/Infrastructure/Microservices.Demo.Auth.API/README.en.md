# Microservices.Demo.Auth.API

Identity service for the demo. Users submit credentials here; Auth API validates them against MongoDB and returns a JWT used by the other services to authorize operations.

## Technology

ASP.NET Core/.NET 6, MongoDB.Driver, JWT, and Steeltoe Config Server/Eureka. The entry point is `Program.cs`; the main controller is `Controllers/UsersController.cs`.

## Endpoints

- `POST /api/users`: allows anonymous authentication and returns a JWT.
- `GET /api/users`: returns the user represented by the token; requires authentication.

Initial demo users are defined in `db/Microservices.Demo.Auth.DB/Docker/init.js`: `erick/erick`, `eva/eva`, and `oscar/oscar`.

## Authentication flow

1. The client sends `POST /api/users` with a username and password.
2. The service looks up the user in the `SecurityDB.Users` collection.
3. If credentials are valid, it creates a JWT containing the user identity and claims.
4. The client sends the token to the Gateway as `Authorization: Bearer <token>`.
5. The Gateway and APIs validate the token before allowing protected operations.

`GET /api/users` is not the login endpoint: it returns the user associated with the current token. A `401` means the token is missing or invalid.

## Dependencies

- MongoDB `microservices.demo.auth.db`, local port `27018`; database `SecurityDB`, collection `Users`.
- Config Server `microservices.demo.configserver:8889`.
- Eureka `microservices.demo.discoveryserver:8761`.
- Logstash, Jaeger, and APM.

It registers as `Microservices.Demo.Auth.API` and listens internally on port `80`.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml build microservices.demo.auth.api
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.auth.api
```

The included credentials are for the demo only and must not be reused.

## Local execution

Requires the .NET 6 SDK and MongoDB available at `localhost:27018`. With Config Server and Eureka running, execute from this directory:

```powershell
dotnet restore
dotnet run
```

For a reproducible environment, run Auth API with `docker-compose-infr.yml`, which also connects it to `backend` and its MongoDB database.
