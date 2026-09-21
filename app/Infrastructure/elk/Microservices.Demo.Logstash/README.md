# Microservices.Demo.Logstash

Logstash es el colector y procesador de logs. Su trabajo es recibir eventos de los servicios, normalizarlos y enviarlos al almacenamiento de búsqueda. En esta demo evita que cada API tenga que conocer los detalles de Elasticsearch.

## Flujo de este proyecto

1. Los servicios envían JSON al endpoint HTTP interno `microservices.demo.logstash:28080`.
2. El pipeline interpreta el cuerpo como JSON.
3. Divide el campo `events` cuando contiene varios eventos.
4. Elimina campos de transporte como `headers`.
5. Escribe los documentos en Elasticsearch con el índice `ms-services-logs`.

## Configuración

- Imagen: `docker.elastic.co/logstash/logstash:8.5.2`.
- Pipeline: `conf.d/logstash.conf`.
- Entrada HTTP: `28080`.
- Puerto `5044`: publicado por Compose, aunque el pipeline actual no configura una entrada Beats para él.
- Salida: `microservices.demo.elasticsearch:9200`.

## Arranque y diagnóstico

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.logstash
docker logs microservices.demo.logstash
curl.exe http://localhost:9200/_cat/indices?v
```

Si Elasticsearch no contiene `ms-services-logs`, revisa conectividad desde la red `backend`, la sintaxis de `conf.d/logstash.conf` y los logs del contenedor.
