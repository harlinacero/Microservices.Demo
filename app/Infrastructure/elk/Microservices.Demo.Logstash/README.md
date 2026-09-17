# Microservices.Demo.Logstash

Pipeline de entrada y transformación de logs HTTP.

- Imagen: `docker.elastic.co/logstash/logstash:8.5.2`.
- Entrada HTTP: puerto interno/local `28080`.
- Entrada adicional publicada: `5044`.
- Configuración: `conf.d/logstash.conf`.
- Salida: Elasticsearch en `microservices.demo.elasticsearch:9200`, índice `ms-services-logs`.

El pipeline usa codec JSON, divide el campo `events` y elimina `headers` antes de enviar el documento. Los servicios .NET y Java envían sus eventos al endpoint HTTP interno.

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.logstash
```

Ver logs con `docker logs microservices.demo.logstash` si los eventos no aparecen en Elasticsearch.
