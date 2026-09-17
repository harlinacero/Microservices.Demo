# Microservices.Demo.Elasticsearch

Servicio de almacenamiento para logs y eventos APM.

- Imagen: `docker.elastic.co/elasticsearch/elasticsearch:8.5.2`.
- Puertos: `9200` para HTTP y `9300` para transporte.
- Modo: nodo único (`discovery.type=single-node`).
- Seguridad desactivada para la demo.
- Datos montados desde `data`.

Logstash escribe el índice `ms-services-logs` y APM Server usa Elasticsearch como salida. La instancia se conecta a la red Docker `backend`.

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.elasticsearch
```

En un entorno real se deben fijar versiones, habilitar seguridad y proteger el volumen de datos.
