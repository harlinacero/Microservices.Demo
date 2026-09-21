# Microservices.Demo.ConfigServer

Servidor centralizado de configuración basado en Spring Cloud Config Server. Evita duplicar configuración dentro de cada imagen y permite que los clientes .NET obtengan sus propiedades desde un repositorio Git común.

## Qué administra

- Identidad y entorno de cada aplicación.
- URL y parámetros de Eureka.
- Cadenas de conexión y nombres de bases.
- Configuración de seguridad.
- Rutas de Ocelot del Gateway.

Config Server no es una base de datos ni un proxy de negocio: lee archivos de configuración desde Git y los expone mediante una API HTTP.

## Configuración

La configuración está en `src/main/resources/application.yml`:

- Puerto: `8889`.
- Repositorio: `https://github.com/harlinacero/Microservices.Demo-config.git`.
- Rama: `main`.
- Carpeta por aplicación: `{application}`.
- Registro en Eureka: `microservices.demo.discoveryserver:8761`.

El repositorio debe contener carpetas con el nombre exacto de cada aplicación, por ejemplo `Microservices.Demo.Client.Web.ApiGateway/application.yml`.

## Flujo de consulta

1. Un cliente se registra/descubre mediante Eureka.
2. Steeltoe solicita `/application/profile/label` a Config Server.
3. Config Server clona o actualiza el repositorio Git.
4. Busca la carpeta de la aplicación y combina sus archivos YAML/properties.
5. Devuelve las propiedades al cliente.

Con `failFast=true`, un error de URL, rama, red o nombre de carpeta impide que el cliente termine de iniciar.

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
