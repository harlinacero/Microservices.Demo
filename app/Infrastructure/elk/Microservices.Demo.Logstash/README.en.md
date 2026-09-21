# Microservices.Demo.Logstash

Logstash is the log collector and processor. It receives service events, normalizes them, and sends them to searchable storage. In this demo it keeps each API from needing to know Elasticsearch details.

## Flow for this project

1. Services send JSON to the internal HTTP endpoint `microservices.demo.logstash:28080`.
2. The pipeline interprets the body as JSON.
3. It splits the `events` field when it contains multiple events.
4. It removes transport fields such as `headers`.
5. It writes documents to Elasticsearch under `ms-services-logs`.

## Configuration

- Image: `docker.elastic.co/logstash/logstash:8.5.2`.
- Pipeline: `conf.d/logstash.conf`.
- HTTP input: `28080`.
- Port `5044`: published by Compose, although the current pipeline does not configure a Beats input for it.
- Output: `microservices.demo.elasticsearch:9200`.

## Start and diagnose

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.logstash
docker logs microservices.demo.logstash
curl.exe http://localhost:9200/_cat/indices?v
```

If Elasticsearch does not contain `ms-services-logs`, check connectivity on `backend`, the syntax of `conf.d/logstash.conf`, and the container logs.
