# Observabilidad: ELK, APM y Jaeger

Esta carpeta agrupa los componentes transversales de logs, trazas y métricas.

## Componentes

- `Microservices.Demo.Elasticsearch`: almacenamiento de logs y datos APM, imagen `docker.elastic.co/elasticsearch/elasticsearch:8.5.2`, puerto `9200`.
- `Microservices.Demo.Kibana`: interfaz de consulta y visualización, imagen `docker.elastic.co/kibana/kibana:8.5.2`, puerto `5601`.
- `Microservices.Demo.Logstash`: recibe JSON por HTTP en `28080` y escribe el índice `ms-services-logs` en Elasticsearch. La configuración está en `conf.d/logstash.conf`.
- `Microservices.Demo.Apm-Server`: recibe eventos APM en `8200` y los envía a Elasticsearch. Su imagen actual es `7.15.2`.

Jaeger no tiene un proyecto de código en esta carpeta; se ejecuta desde Compose con `jaegertracing/all-in-one:1.40` y su UI está en `http://localhost:16686`.

## Arranque

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.elasticsearch microservices.demo.logstash microservices.demo.apm-server microservices.demo.jaeger microservices.demo.kibana
```

Los APIs .NET y Java envían logs a `microservices.demo.logstash:28080` y trazas/métricas según sus variables OpenTelemetry y configuración APM.

## Advertencias

El stack mezcla Elasticsearch/Kibana/Logstash `8.5.2` con APM Server `7.15.2`. Para un entorno mantenible conviene usar versiones compatibles y configurar seguridad fuera de los valores de demo.
