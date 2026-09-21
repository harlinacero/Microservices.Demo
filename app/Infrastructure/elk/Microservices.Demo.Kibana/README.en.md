# Microservices.Demo.Kibana

Kibana is the web analysis interface for the Elastic stack. It does not receive logs directly: it queries the indexes Elasticsearch stores after Logstash processes them. It is used to investigate errors, compare timings, create visualizations, and build dashboards.

## What you can do

- Search messages by service, level, date, or text.
- Filter requests from a specific microservice.
- Create a Data View exclusively for `ms-services-logs`.
- Build charts for log and error volume.
- Correlate logs with the time an incident occurred.

## Configuration

- Image: `docker.elastic.co/kibana/kibana:8.5.2`.
- Local port: `5601`.
- Elasticsearch: `http://microservices.demo.elasticsearch:9200`.

## Use

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.kibana
```

Open `http://localhost:5601` and create a Data View with:

- Name: `Microservices logs`.
- Pattern: `ms-services-logs`.
- Time field: `@timestamp`.

Then open Discover, select `Microservices logs`, and choose a range such as `Last 24 hours`. Do not reuse a Data View that includes `apm-*` if any of those indexes has unavailable shards; Discover will query every index in the pattern.

## Diagnostics

If Kibana does not open, check Elasticsearch first. If it opens but no data appears, check Logstash and confirm the index exists:

```powershell
curl.exe http://localhost:9200/_cat/indices?v
curl.exe http://localhost:9200/ms-services-logs/_count
docker logs microservices.demo.logstash
```

If `_count` returns documents but Discover reports an APM error, verify that `Microservices logs` is selected instead of `security-solution-default`.
