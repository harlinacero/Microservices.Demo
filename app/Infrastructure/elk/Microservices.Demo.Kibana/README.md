# Microservices.Demo.Kibana

Interfaz web para explorar índices y visualizar los datos almacenados en Elasticsearch.

- Imagen: `docker.elastic.co/kibana/kibana:8.5.2`.
- Puerto local: `5601`.
- Elasticsearch configurado como `http://microservices.demo.elasticsearch:9200`.

Kibana depende de Elasticsearch y se inicia desde `docker-compose-infr.yml`.

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.kibana
```

Abrir `http://localhost:5601` y crear un data view para `ms-services-logs` cuando Logstash haya recibido eventos.
