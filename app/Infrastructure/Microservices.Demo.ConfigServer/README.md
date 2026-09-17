# Microservices.Demo.ConfigServer

Servidor centralizado de configuración basado en Spring Cloud Config Server. Los clientes .NET consultan aquí sus propiedades de conexión, Eureka, seguridad y rutas del Gateway.

## Configuración

La configuración está en `src/main/resources/application.yml`:

- Puerto: `8889`.
- Repositorio: `https://github.com/harlinacero/Microservices.Demo-config.git`.
- Rama: `main`.
- Carpeta por aplicación: `{application}`.
- Registro en Eureka: `microservices.demo.discoveryserver:8761`.

El repositorio debe contener carpetas con el nombre exacto de cada aplicación, por ejemplo `Microservices.Demo.Client.Web.ApiGateway/application.yml`.

## Consultas útiles

```powershell
curl.exe http://localhost:8889/Microservices.Demo.Client.Web.ApiGateway/Production
curl.exe http://localhost:8889/Microservices.Demo.Auth.API/Production
```

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml build microservices.demo.configserver
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.configserver
```

El Dockerfile usa Maven/Java 17 y descarga el agente OpenTelemetry durante el build. Si un cliente falla con `Could not locate PropertySource`, revisar primero URL, rama, nombre de carpeta y logs de este contenedor.
