# Microservices.Demo.Apm-Server

APM significa **Application Performance Monitoring**. APM Server recibe la telemetría que producen los agentes instalados en las aplicaciones: transacciones, duración de llamadas, excepciones, dependencias y metadatos del servicio. Su función es actuar como puerta de entrada de APM, no como interfaz de consulta.

## Qué permite observar

- Qué endpoint es lento.
- Cuánto tiempo tarda una llamada entre microservicios.
- Qué excepciones se producen y en qué servicio.
- Qué dependencias externas intervienen en una transacción.
- Nombre y entorno de cada servicio instrumentado.

## Flujo

```text
Aplicación Java/.NET -> agente APM/OpenTelemetry -> APM Server:8200 -> Elasticsearch -> Kibana/APM
```

- Imagen: `docker.elastic.co/apm/apm-server:7.15.2`.
- Puerto local: `8200`.
- Configuración: `apm-server.yml`.
- Salida: `microservices.demo.elasticsearch:9200`.

Los servicios Java usan `opentelemetry-javaagent.jar`; los servicios .NET usan paquetes Elastic APM/OpenTelemetry. Jaeger cumple una función relacionada, pero muestra trazas distribuidas y usa su propio backend.

## Arranque y diagnóstico

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml build microservices.demo.apm-server
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.apm-server
docker logs microservices.demo.apm-server
```

Si aparecen errores de exportación, comprueba primero Elasticsearch y la URL configurada. La demo mezcla APM `7.15.2` con Elastic `8.5.2`; conviene alinear versiones antes de usarla en producción.
