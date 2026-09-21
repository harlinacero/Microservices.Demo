# Microservices.Demo.Apm-Server

**APM** means **Application Performance Monitoring**. APM Server receives telemetry produced by agents installed in applications: transactions, call duration, exceptions, dependencies, and service metadata. Its role is to be the APM ingestion endpoint, not the query interface.

## What it observes

- Which endpoint is slow.
- How long a call between microservices takes.
- Which exceptions occur and in which service.
- Which external dependencies participate in a transaction.
- The name and environment of each instrumented service.

## Flow

```text
Java/.NET application -> APM/OpenTelemetry agent -> APM Server:8200 -> Elasticsearch -> Kibana/APM
```

- Image: `docker.elastic.co/apm/apm-server:7.15.2`.
- Local port: `8200`.
- Configuration: `apm-server.yml`.
- Output: `microservices.demo.elasticsearch:9200`.

Java services use `opentelemetry-javaagent.jar`; .NET services use Elastic APM/OpenTelemetry packages. Jaeger has a related purpose, but displays distributed traces and uses its own backend.

## Start and diagnose

```powershell
docker-compose -f ..\..\..\..\docker-compose-infr.yml build microservices.demo.apm-server
docker-compose -f ..\..\..\..\docker-compose-infr.yml up -d microservices.demo.apm-server
docker logs microservices.demo.apm-server
```

For export errors, check Elasticsearch and the configured URL first. The demo mixes APM `7.15.2` with Elastic `8.5.2`; align versions before production use.
