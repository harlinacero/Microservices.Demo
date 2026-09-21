# Observability: ELK, APM, and Jaeger

This directory contains the tools that show what is happening inside the microservices without connecting to each container separately. This demo has three observability signals:

- **Logs**: application messages and HTTP events.
- **Traces**: the path of a request across services.
- **Metrics/APM**: timings, errors, transactions, and performance.

## Data flow

```text
.NET/Java APIs --HTTP logs:28080--> Logstash --> Elasticsearch --> Kibana
.NET/Java APIs --APM:8200-------> APM Server --> Elasticsearch
.NET/Java APIs --tracing--------> Jaeger --> Jaeger UI
```

## What each component does

| Component     | Purpose                                                          |
| ------------- | ---------------------------------------------------------------- |
| Elasticsearch | Stores and indexes logs and events for fast searching.           |
| Logstash      | Receives, transforms, and routes logs to Elasticsearch.          |
| Kibana        | Searches logs and creates filters, charts, and dashboards.       |
| APM Server    | Receives performance and error telemetry from APM agents.        |
| Jaeger        | Displays distributed traces: one request and its internal calls. |

## Start

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.elasticsearch microservices.demo.logstash microservices.demo.apm-server microservices.demo.jaeger microservices.demo.kibana
```

## Investigate a problem

1. Check that the container producing the event is running.
2. Read its logs with `docker logs <container>`.
3. In Kibana use a Data View with the exact pattern `ms-services-logs` and `@timestamp` as the time field.
4. In Jaeger search by service name and inspect call duration.
5. In APM inspect errors and slow transactions.

## Ports

- Kibana: `http://localhost:5601`.
- Elasticsearch: `http://localhost:9200`.
- APM Server: `http://localhost:8200`.
- Jaeger UI: `http://localhost:16686`.
- Logstash HTTP: `28080`.

The stack mixes Elasticsearch/Kibana/Logstash `8.5.2` with APM Server `7.15.2`. Align versions, enable security, and protect data in production.

If Discover reports an `apm-*` index error, switch to the logs-only Data View. A pattern that mixes `ms-services-logs` with APM can fail even when the logs index is available.
