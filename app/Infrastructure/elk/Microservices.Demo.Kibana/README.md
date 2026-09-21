# Microservices.Demo.Kibana

Kibana es la interfaz web de análisis del stack Elastic. No recibe directamente los logs: consulta los índices que Elasticsearch guarda después de que Logstash los procesa. Sirve para investigar errores, comparar tiempos, crear visualizaciones y construir dashboards.

## Qué puedes hacer

- Buscar mensajes por servicio, nivel, fecha o texto.
- Filtrar peticiones de un microservicio concreto.
- Crear una Data View exclusiva para `ms-services-logs`.
- Construir gráficos de volumen de logs y errores.
- Correlacionar logs con el momento en que ocurrió una incidencia.

## Configuración

- Imagen: `docker.elastic.co/kibana/kibana:8.5.2`.
- Puerto local: `5601`.
- Elasticsearch: `http://microservices.demo.elasticsearch:9200`.

## Uso

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.kibana
```

Abre `http://localhost:5601` y crea una Data View con estos valores:

- Nombre: `Microservices logs`.
- Patrón: `ms-services-logs`.
- Campo temporal: `@timestamp`.

Después entra en Discover, selecciona `Microservices logs` y elige un rango como `Last 24 hours`. No reutilices una Data View que incluya `apm-*` si alguno de esos índices tiene shards no disponibles; Discover intentará consultar todos los índices del patrón.

## Diagnóstico

Si Kibana no abre, revisa Elasticsearch primero. Si abre pero no hay datos, revisa Logstash y comprueba que el índice exista:

```powershell
curl.exe http://localhost:9200/_cat/indices?v
curl.exe http://localhost:9200/ms-services-logs/_count
docker logs microservices.demo.logstash
```

Si `_count` devuelve documentos pero Discover muestra un error de APM, revisa que esté seleccionada la Data View `Microservices logs` y no `security-solution-default`.
