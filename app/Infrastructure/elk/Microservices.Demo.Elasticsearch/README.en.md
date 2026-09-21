# Microservices.Demo.Elasticsearch

Elasticsearch is the central observability store and index. It stores log documents sent by Logstash and performance events sent by APM Server, allowing searches by fields, text, and time.

## What it provides

- Fast log searches by service, exception, or date.
- Separate indexes for different event types.
- An HTTP API for checking health and indexes.
- The backend for Kibana, which does not store the data itself.

## Demo configuration

- Image: `docker.elastic.co/elasticsearch/elasticsearch:8.5.2`.
- HTTP: `9200`; transport: `9300`.
- Single node: `discovery.type=single-node`.
- Security disabled.
- Data mounted from `data`, a runtime directory ignored by Git.

Because this is a single-node cluster, indexes configured with a replica may be `yellow`; that is normal while the primary shard is `STARTED`. A `red` state means a primary shard is missing and Kibana or affected searches may fail.

## Use

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.elasticsearch
curl.exe http://localhost:9200
curl.exe http://localhost:9200/_cat/indices?v
curl.exe http://localhost:9200/ms-services-logs/_count
```

Kibana queries this service, while Logstash/APM Server write to it. If it is down, log searches and APM ingestion usually stop, although business APIs may continue serving requests.

Application logs are stored in `ms-services-logs`. Its Kibana Data View must point exactly to that index and use `@timestamp`; `apm-*` indexes belong to APM Server and should not be mixed into the logs view if any of them is `red`.

For production, pin versions, enable authentication/TLS, and use managed persistent storage.
