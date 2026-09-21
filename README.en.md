# Microservices.Demo

Educational demonstration of a microservices architecture for an insurance platform. The project shows how to separate authentication, products, pricing, policies, and reporting, and how to connect them through service discovery, centralized configuration, an API Gateway, messaging, and observability.

## Architecture

```text
Angular Client (8081) -> API Gateway / Ocelot (44399)
                                                            |
                            +---------------+----------------+
                            v               v                v
                    Auth API       Product API       Policy API -> Pricing API
                    MongoDB        SQL Server        SQL Server + RabbitMQ
                                                            |
                                                    Report API

Config Server (8889) <- Git: Microservices.Demo-config
Discovery Server (8761) <- Eureka registry
APIs -> Logstash -> Elasticsearch -> Kibana
APIs -> Jaeger / APM Server
```

All containers connect to the Docker `backend` network. Compose service names work as internal DNS names.

## Organization

### `app/Client`

- `Microservices.Demo.Client.Web`: Angular 14 application served by Nginx. It consumes the Gateway.

### `app/Domain`

- `Microservices.Demo.Product.API`: product catalog and lifecycle; SQL Server and EF Core.
- `Microservices.Demo.Pricing.API`: pricing calculation and lookup; PostgreSQL and Marten.
- `Microservices.Demo.Policy.API`: policy creation, lookup, and termination; SQL Server, RabbitMQ, and Pricing.
- `Microservices.Demo.Report.API`: reports that compose Product and Policy information.

### `app/Infrastructure`

- `Microservices.Demo.Auth.API`: authentication and JWT; MongoDB.
- `Microservices.Demo.Client.Web.ApiGateway`: public HTTP entry point with Ocelot, Eureka, and JWT.
- `Microservices.Demo.ConfigServer`: Spring Cloud Config Server connected to the external Git repository `https://github.com/harlinacero/Microservices.Demo-config.git`, branch `main`.
- `Microservices.Demo.DiscoveryServer`: Eureka server.
- `elk`: APM Server, Elasticsearch, Kibana, and Logstash.

### `db`

- `Microservices.Demo.Auth.DB`: MongoDB with demo users.
- `Microservices.Demo.Policy.DB`: SQL Server 2019 for policies.
- `Microservices.Demo.Pricing.DB`: PostgreSQL for pricing.
- `Microservices.Demo.Product.DB`: SQL Server 2019 for products.

## Request flow

1. The browser loads `http://localhost:8081`.
2. The client calls `http://localhost:44399`.
3. Ocelot gets its routes from Config Server and discovers destinations through Eureka.
4. The .NET APIs load configuration from Config Server and register with Eureka. `failFast=true` makes them stop if Config Server is unavailable.
5. `POST /api/users` authenticates a user and returns a JWT. Protected operations require `Authorization: Bearer <token>`.
6. Product, Pricing, and Policy access their own databases. Policy uses RabbitMQ and may call Pricing.
7. Report calls other APIs through Eureka.

## Recommended startup

```powershell
docker-compose -f .\docker-compose-infr.yml build
docker-compose -f .\docker-compose-infr.yml up -d

docker-compose -f .\docker-compose-db.yml build
docker-compose -f .\docker-compose-db.yml up -d

docker-compose -f .\docker-compose-all.yml build
docker-compose -f .\docker-compose-all.yml up -d
```

Check the state before testing the application:

```powershell
docker-compose -f .\docker-compose-infr.yml ps
docker-compose -f .\docker-compose-db.yml ps
docker-compose -f .\docker-compose-all.yml ps
```

Compose file scopes:

| File                      | Contents                                                             |
| ------------------------- | -------------------------------------------------------------------- |
| `docker-compose-infr.yml` | Eureka, Config Server, Gateway, Auth, RabbitMQ, ELK, APM, and Jaeger |
| `docker-compose-db.yml`   | All four databases                                                   |
| `docker-compose-app.yml`  | Domain APIs and Angular client                                       |
| `docker-compose.yml`      | Domain APIs without infrastructure or databases                      |
| `docker-compose-all.yml`  | Infrastructure, APIs, and client, but not databases                  |

## Ports

| Component           | Local port |
| ------------------- | ---------: |
| Angular client      |     `8081` |
| API Gateway         |    `44399` |
| Eureka              |     `8761` |
| Config Server       |     `8889` |
| Auth MongoDB        |    `27018` |
| Policy SQL Server   |    `14332` |
| Pricing PostgreSQL  |    `54321` |
| Product SQL Server  |    `14331` |
| RabbitMQ Management |    `15672` |
| Elasticsearch       |     `9200` |
| Kibana              |     `5601` |
| Logstash HTTP       |    `28080` |
| APM Server          |     `8200` |
| Jaeger UI           |    `16686` |

Replicated APIs publish internal port `80` dynamically; use the Gateway to access them.

## Quick tests

Get a demo token:

```powershell
curl.exe -i -X POST http://localhost:44399/api/users `
    -H "Content-Type: application/json" `
    -d '{"username":"erick","password":"erick"}'
```

Query the authenticated user:

```powershell
curl.exe -i http://localhost:44399/api/users -H "Authorization: Bearer <token>"
```

To diagnose a request, inspect the three main hops:

```powershell
docker logs Microservices.Demo.Client.Web.ApiGateway
docker logs Microservices.Demo.Auth.Api
docker logs Microservices.Demo.ConfigServer
```

## External configuration

Ocelot routes are not stored in this repository. The external Git repository must contain `Microservices.Demo.Client.Web.ApiGateway/application.yml` on branch `main`. This dependency is required for the Gateway to start.

## Observability

Logstash receives HTTP events on `28080` and writes the `ms-services-logs` index to Elasticsearch. Kibana is at `http://localhost:5601`, Jaeger at `http://localhost:16686`, and APM Server at `http://localhost:8200`.

Elastic images are `8.5.2`, while APM Server uses `7.15.2`; align versions for production.

## Warnings

- SQL Server, RabbitMQ, and Mongo users use demo passwords in plain text.
- The project uses .NET 6 and Java 17.
- `docker-compose-all.yml` does not start databases; also use `docker-compose-db.yml`.
- Docker Compose reports `version: '3.4'` as obsolete, although it is still accepted.
- Some volume paths depend on Windows-tolerated case differences.

## Project documentation

Each Spanish README has a matching English `README.en.md` in the same directory.

- [Angular client](app/Client/Microservices.Demo.Client.Web/README.en.md)
- [Product API](app/Domain/Microservices.Demo.Product.API/README.en.md)
- [Pricing API](app/Domain/Microservices.Demo.Pricing.API/README.en.md)
- [Policy API](app/Domain/Microservices.Demo.Policy.API/README.en.md)
- [Report API](app/Domain/Microservices.Demo.Report.API/README.en.md)
- [Auth API](app/Infrastructure/Microservices.Demo.Auth.API/README.en.md)
- [API Gateway](app/Infrastructure/Microservices.Demo.Client.Web.ApiGateway/README.en.md)
- [Config Server](app/Infrastructure/Microservices.Demo.ConfigServer/README.en.md)
- [Discovery Server](app/Infrastructure/Microservices.Demo.DiscoveryServer/README.en.md)
- [Observability](app/Infrastructure/elk/README.en.md)
- [Elasticsearch](app/Infrastructure/elk/Microservices.Demo.Elasticsearch/README.en.md)
- [Kibana](app/Infrastructure/elk/Microservices.Demo.Kibana/README.en.md)
- [Logstash](app/Infrastructure/elk/Microservices.Demo.Logstash/README.en.md)
- [APM Server](app/Infrastructure/elk/Microservices.Demo.Apm-Server/README.en.md)
- [Databases](db/README.en.md)
- [Auth DB](db/Microservices.Demo.Auth.DB/README.en.md)
- [Policy DB](db/Microservices.Demo.Policy.DB/README.en.md)
- [Pricing DB](db/Microservices.Demo.Pricing.DB/README.en.md)
- [Product DB](db/Microservices.Demo.Product.DB/README.en.md)
