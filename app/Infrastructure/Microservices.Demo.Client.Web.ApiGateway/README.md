# Microservices.Demo.Client.Web.ApiGateway

El API Gateway es la puerta de entrada única para el cliente web. Centraliza el acceso HTTP, evita exponer cada API interna, valida JWT y reenvía cada petición al microservicio correcto. Está construido con ASP.NET Core/.NET 6, Ocelot y el proveedor Eureka.

## Flujo de configuración

1. `Program.cs` llama a `AddConfigServer(...)`.
2. Steeltoe descubre Config Server mediante Eureka.
3. Config Server lee `Microservices.Demo-config` desde Git.
4. Ocelot recibe las rutas y usa Eureka para localizar `ServiceName`.

El repositorio externo debe tener `Microservices.Demo.Client.Web.ApiGateway/application.yml` en la rama `main`.

## Qué resuelve

- Oculta direcciones y puertos internos de los microservicios.
- Proporciona una URL pública estable (`44399`).
- Aplica autenticación y requisitos de claims por ruta.
- Usa descubrimiento de servicios para localizar instancias dinámicas.
- Permite cambiar rutas sin recompilar el Gateway.

## Rutas principales

La configuración externa define `/api/users`, `/api/products`, `/api/offers`, `/api/policies` y `/api/report/policies`. Auth usa POST anónimo para login; las consultas protegidas usan JWT y claims de tipo `SALESMAN` cuando la ruta lo exige.

## Flujo de una petición

```text
Cliente -> Gateway -> Configuración Ocelot -> Eureka -> API destino -> Base de datos
```

El Gateway no ejecuta la lógica de productos o pólizas. Si una petición devuelve `401`, normalmente falló la autenticación; si devuelve `502` o `503`, revisa Eureka y el servicio destino. Si el socket se cierra durante el arranque, revisa Config Server.

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
