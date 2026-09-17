# Microservices.Demo

Demo educativa de una arquitectura de microservicios para una plataforma de seguros. El proyecto muestra cómo separar un sistema en servicios de autenticación, productos, precios, pólizas y reportes, y cómo conectarlos mediante descubrimiento de servicios, configuración centralizada, un API Gateway, mensajería y observabilidad.

## Arquitectura

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

Todos los contenedores se conectan a la red Docker `backend`. Los nombres de servicio de Compose funcionan como DNS interno.

## Organización

### `app/Client`

- `Microservices.Demo.Client.Web`: aplicación Angular 14 servida por Nginx. Consume el Gateway.

### `app/Domain`

- `Microservices.Demo.Product.API`: catálogo y ciclo de vida de productos; SQL Server y EF Core.
- `Microservices.Demo.Pricing.API`: cálculo y consulta de precios; PostgreSQL y Marten.
- `Microservices.Demo.Policy.API`: creación, consulta y terminación de pólizas; SQL Server, RabbitMQ y Pricing.
- `Microservices.Demo.Report.API`: reportes que componen información de Product y Policy.

### `app/Infrastructure`

- `Microservices.Demo.Auth.API`: autenticación y JWT; MongoDB.
- `Microservices.Demo.Client.Web.ApiGateway`: entrada HTTP pública con Ocelot, Eureka y JWT.
- `Microservices.Demo.ConfigServer`: Spring Cloud Config Server conectado al repositorio Git externo `https://github.com/harlinacero/Microservices.Demo-config.git`, rama `main`.
- `Microservices.Demo.DiscoveryServer`: servidor Eureka.
- `elk`: APM Server, Elasticsearch, Kibana y Logstash.

### `db`

- `Microservices.Demo.Auth.DB`: MongoDB con usuarios demo.
- `Microservices.Demo.Policy.DB`: SQL Server 2019 para pólizas.
- `Microservices.Demo.Pricing.DB`: PostgreSQL para precios.
- `Microservices.Demo.Product.DB`: SQL Server 2019 para productos.

## Flujo de una petición

1. El navegador carga `http://localhost:8081`.
2. El cliente llama a `http://localhost:44399`.
3. Ocelot obtiene sus rutas desde Config Server y descubre los destinos mediante Eureka.
4. Los APIs .NET cargan configuración desde Config Server y se registran en Eureka. `failFast=true` hace que terminen si Config Server no está disponible.
5. `POST /api/users` autentica un usuario y devuelve un JWT. Las operaciones protegidas requieren `Authorization: Bearer <token>`.
6. Product, Pricing y Policy acceden a sus propias bases. Policy usa RabbitMQ para mensajería y puede llamar a Pricing.
7. Report consulta otros APIs mediante Eureka.

## Arranque recomendado

```powershell
docker-compose -f .\docker-compose-infr.yml build
docker-compose -f .\docker-compose-infr.yml up -d

docker-compose -f .\docker-compose-db.yml build
docker-compose -f .\docker-compose-db.yml up -d

docker-compose -f .\docker-compose-all.yml build
docker-compose -f .\docker-compose-all.yml up -d
```

Comprueba el estado antes de probar la aplicación:

```powershell
docker-compose -f .\docker-compose-infr.yml ps
docker-compose -f .\docker-compose-db.yml ps
docker-compose -f .\docker-compose-all.yml ps
```

Los Compose tienen estos alcances:

| Archivo                   | Contenido                                                         |
| ------------------------- | ----------------------------------------------------------------- |
| `docker-compose-infr.yml` | Eureka, Config Server, Gateway, Auth, RabbitMQ, ELK, APM y Jaeger |
| `docker-compose-db.yml`   | Las cuatro bases de datos                                         |
| `docker-compose-app.yml`  | APIs de dominio y cliente Angular                                 |
| `docker-compose.yml`      | APIs de dominio sin infraestructura ni bases                      |
| `docker-compose-all.yml`  | Infraestructura, APIs y cliente, pero no las bases                |

## Puertos

| Componente          | Puerto local |
| ------------------- | -----------: |
| Cliente Angular     |       `8081` |
| API Gateway         |      `44399` |
| Eureka              |       `8761` |
| Config Server       |       `8889` |
| MongoDB Auth        |      `27018` |
| SQL Server Policy   |      `14332` |
| PostgreSQL Pricing  |      `54321` |
| SQL Server Product  |      `14331` |
| RabbitMQ Management |      `15672` |
| Elasticsearch       |       `9200` |
| Kibana              |       `5601` |
| Logstash HTTP       |      `28080` |
| APM Server          |       `8200` |
| Jaeger UI           |      `16686` |

Los APIs replicados publican el puerto interno `80` de forma dinámica; usa el Gateway para accederlos.

## Pruebas rápidas

Obtener un token demo:

```powershell
curl.exe -i -X POST http://localhost:44399/api/users `
	-H "Content-Type: application/json" `
	-d '{"username":"erick","password":"erick"}'
```

Consultar el usuario autenticado:

```powershell
curl.exe -i http://localhost:44399/api/users -H "Authorization: Bearer <token>"
```

Para diagnosticar una petición, revisar los tres saltos:

```powershell
docker logs Microservices.Demo.Client.Web.ApiGateway
docker logs Microservices.Demo.Auth.Api
docker logs Microservices.Demo.ConfigServer
```

## Configuración externa

Las rutas de Ocelot no están en este repositorio. El repositorio Git externo debe contener `Microservices.Demo.Client.Web.ApiGateway/application.yml` y una rama `main`. Esta dependencia es necesaria para iniciar el Gateway.

## Observabilidad

Logstash recibe eventos HTTP en `28080` y escribe el índice `ms-services-logs` en Elasticsearch. Kibana está en `http://localhost:5601`, Jaeger en `http://localhost:16686` y APM Server en `http://localhost:8200`.

Las imágenes Elastic están en `8.5.2`, mientras APM Server usa `7.15.2`; para producción conviene alinear las versiones.

## Advertencias

- Las contraseñas de SQL Server, RabbitMQ y usuarios Mongo son valores de demo en texto plano.
- El proyecto usa .NET 6 y Java 17.
- `docker-compose-all.yml` no inicia las bases; usa también `docker-compose-db.yml`.
- Docker Compose muestra `version: '3.4'` como obsoleto, aunque todavía lo acepta.
- Algunas rutas de volumen dependen de mayúsculas/minúsculas toleradas por Windows.

## Documentación por proyecto

- [Cliente Angular](app/Client/Microservices.Demo.Client.Web/README.md)
- [Product API](app/Domain/Microservices.Demo.Product.API/README.md)
- [Pricing API](app/Domain/Microservices.Demo.Pricing.API/README.md)
- [Policy API](app/Domain/Microservices.Demo.Policy.API/README.md)
- [Report API](app/Domain/Microservices.Demo.Report.API/README.md)
- [Auth API](app/Infrastructure/Microservices.Demo.Auth.API/README.md)
- [API Gateway](app/Infrastructure/Microservices.Demo.Client.Web.ApiGateway/README.md)
- [Config Server](app/Infrastructure/Microservices.Demo.ConfigServer/README.md)
- [Discovery Server](app/Infrastructure/Microservices.Demo.DiscoveryServer/README.md)
- [Observabilidad](app/Infrastructure/elk/README.md)
- [Elasticsearch](app/Infrastructure/elk/Microservices.Demo.Elasticsearch/README.md)
- [Kibana](app/Infrastructure/elk/Microservices.Demo.Kibana/README.md)
- [Logstash](app/Infrastructure/elk/Microservices.Demo.Logstash/README.md)
- [APM Server](app/Infrastructure/elk/Microservices.Demo.Apm-Server/README.md)
- [Bases de datos](db/README.md)
- [Auth DB](db/Microservices.Demo.Auth.DB/README.md)
- [Policy DB](db/Microservices.Demo.Policy.DB/README.md)
- [Pricing DB](db/Microservices.Demo.Pricing.DB/README.md)
- [Product DB](db/Microservices.Demo.Product.DB/README.md)
