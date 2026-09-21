# Observabilidad: ELK, APM y Jaeger

Esta carpeta contiene las herramientas que permiten saber qué ocurre dentro de los microservicios sin tener que conectarse a cada contenedor por separado. La observabilidad de esta demo tiene tres señales:

- **Logs**: mensajes de aplicación y eventos HTTP.
- **Trazas**: recorrido de una petición entre servicios.
- **Métricas/APM**: tiempos, errores, transacciones y rendimiento.

## Flujo de datos

```text
APIs .NET/Java --logs HTTP:28080--> Logstash --> Elasticsearch --> Kibana
APIs .NET/Java --APM:8200-------> APM Server --> Elasticsearch
APIs .NET/Java --tracing--------> Jaeger --> Jaeger UI
```

## ¿Para qué sirve cada componente?

| Componente    | Propósito                                                                      |
| ------------- | ------------------------------------------------------------------------------ |
| Elasticsearch | Guarda e indexa logs y eventos para poder buscarlos rápidamente.               |
| Logstash      | Recibe, transforma y enruta los logs hacia Elasticsearch.                      |
| Kibana        | Permite buscar logs, crear filtros, gráficos y dashboards sobre Elasticsearch. |
| APM Server    | Recibe telemetría de rendimiento y errores de los agentes APM.                 |
| Jaeger        | Muestra trazas distribuidas: una petición y sus llamadas internas.             |

## Arranque

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.elasticsearch microservices.demo.logstash microservices.demo.apm-server microservices.demo.jaeger microservices.demo.kibana
```

## Cómo investigar un problema

1. Comprueba que el contenedor que origina el evento esté activo.
2. Busca sus logs con `docker logs <contenedor>`.
3. En Kibana usa una Data View con el patrón exacto `ms-services-logs` y `@timestamp` como campo temporal.
4. En Jaeger busca por nombre de servicio y revisa la duración de cada llamada.
5. En APM revisa errores y transacciones lentas.

## Puertos

- Kibana: `http://localhost:5601`.
- Elasticsearch: `http://localhost:9200`.
- APM Server: `http://localhost:8200`.
- Jaeger UI: `http://localhost:16686`.
- Logstash HTTP: `28080`.

El stack mezcla Elasticsearch/Kibana/Logstash `8.5.2` con APM Server `7.15.2`. Para producción conviene alinear versiones, habilitar seguridad y proteger los datos.

Si Discover muestra un error sobre un índice `apm-*`, cambia a la Data View exclusiva de logs. Un patrón que mezcle `ms-services-logs` con APM puede fallar aunque el índice de logs esté disponible.
