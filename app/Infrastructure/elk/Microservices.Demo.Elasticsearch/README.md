# Microservices.Demo.Elasticsearch

Elasticsearch es el almacén e índice central de la observabilidad. Guarda documentos de logs enviados por Logstash y eventos de rendimiento enviados por APM Server, y permite buscarlos por campos, texto y tiempo.

## Qué aporta

- Búsqueda rápida de logs por servicio, excepción o fecha.
- Índices separados para distintos tipos de eventos.
- API HTTP para comprobar salud e índices.
- Backend de Kibana, que no almacena los datos por sí mismo.

## Configuración de la demo

- Imagen: `docker.elastic.co/elasticsearch/elasticsearch:8.5.2`.
- HTTP: `9200`; transporte: `9300`.
- Nodo único: `discovery.type=single-node`.
- Seguridad desactivada.
- Datos montados desde `data`, directorio ignorado por Git porque es estado runtime.

Al ser un nodo único, los índices configurados con una réplica pueden aparecer en estado `yellow`; es normal mientras el shard primario esté `STARTED`. Un estado `red` indica que falta un shard primario y Kibana o las búsquedas afectadas pueden fallar.

## Uso

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.elasticsearch
curl.exe http://localhost:9200
curl.exe http://localhost:9200/_cat/indices?v
curl.exe http://localhost:9200/ms-services-logs/_count
```

Kibana consulta este servicio y Logstash/APM Server escriben en él. Si está caído, normalmente dejarán de funcionar las búsquedas de logs y la recepción de APM, aunque los APIs de negocio pueden seguir atendiendo peticiones.

Los logs de la aplicación se almacenan en `ms-services-logs`. Su Data View de Kibana debe apuntar exactamente a ese índice y usar `@timestamp`; los índices `apm-*` pertenecen al APM Server y no deben mezclarse con la vista de logs si alguno está en estado `red`.

En producción se deben fijar versiones, habilitar autenticación/TLS y usar almacenamiento persistente administrado.
