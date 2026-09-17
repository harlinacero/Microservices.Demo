# Microservices.Demo.Client.Web.ApiGateway

Punto de entrada HTTP de la aplicación. El Gateway está construido con ASP.NET Core/.NET 6 y Ocelot; no contiene las rutas de negocio localmente, sino que las carga desde Config Server.

## Flujo de configuración

1. `Program.cs` llama a `AddConfigServer(...)`.
2. Steeltoe descubre Config Server mediante Eureka.
3. Config Server lee `Microservices.Demo-config` desde Git.
4. Ocelot recibe las rutas y usa Eureka para localizar `ServiceName`.

El repositorio externo debe tener `Microservices.Demo.Client.Web.ApiGateway/application.yml` en la rama `main`.

## Rutas principales

La configuración externa define `/api/users`, `/api/products`, `/api/offers`, `/api/policies` y `/api/report/policies`. Auth usa POST anónimo para login; las consultas protegidas usan JWT y claims de tipo `SALESMAN` cuando la ruta lo exige.

## Puertos y ejecución

El contenedor publica `44399:80`:

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml build microservices.demo.client.web.apigateway
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.client.web.apigateway
```

## Diagnóstico

Si el socket se cierra o el contenedor reinicia, revisar:

```powershell
docker logs Microservices.Demo.ConfigServer
docker logs Microservices.Demo.Client.Web.ApiGateway
curl.exe -i http://localhost:8889/Microservices.Demo.Client.Web.ApiGateway/Production
```

`failFast=true` hace que el Gateway termine si Config Server no puede cargar el repositorio o si la rama configurada no existe.
