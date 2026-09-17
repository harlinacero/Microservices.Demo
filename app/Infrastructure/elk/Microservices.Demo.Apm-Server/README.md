# Microservices.Demo.Apm-Server

Servidor Elastic APM para recibir telemetría de las aplicaciones Java y .NET.

- Imagen base: `docker.elastic.co/apm/apm-server:7.15.2`.
- Puerto local: `8200`.
- Configuración: `apm-server.yml`.
- Salida Elasticsearch: `microservices.demo.elasticsearch:9200`.

El Dockerfile instala la configuración propia y ejecuta el agente APM. Los servicios Java usan `opentelemetry-javaagent.jar`; los servicios .NET usan paquetes Elastic APM/OpenTelemetry.

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml build microservices.demo.apm-server
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.apm-server
```

La versión del APM Server debe alinearse con Elasticsearch para un entorno de producción; la demo actual mezcla APM `7.15.2` con Elastic `8.5.2`.
