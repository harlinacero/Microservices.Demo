# Diagrama de componentes

Este documento describe cómo se conectan los componentes de `Microservices.Demo`. Todos los contenedores de Compose comparten la red Docker `backend`; por eso pueden comunicarse usando sus nombres de servicio como DNS interno.

## Vista general

```mermaid
flowchart LR
    Browser[ navegador ] --> Client[Client Web\nAngular 14 + Nginx\n:8081 ]
    Client --> Gateway[API Gateway\nASP.NET Core + Ocelot\n:44399]

    Gateway --> Auth[Auth API\nJWT]
    Gateway --> Product[Product API\nCatalogo de productos]
    Gateway --> Pricing[Pricing API\nCalculo de precios]
    Gateway --> Policy[Policy API\nPolizas y ofertas]
    Gateway --> Report[Report API\nReportes]

    Auth --> AuthDB[(MongoDB\nUsuarios)]
    Product --> ProductDB[(SQL Server\nProductos)]
    Pricing --> PricingDB[(PostgreSQL\nPrecios)]
    Policy --> PolicyDB[(SQL Server\nPolizas)]

    Policy -->|calcula precios| Pricing
    Policy -->|publica eventos| Rabbit[RabbitMQ\n:5672 / :15672]
    Report -->|consulta productos| Product
    Report -->|consulta polizas| Policy

    Config[Config Server\nSpring Cloud Config\n:8889] -. configuracion .-> Gateway
    Config -. configuracion .-> Auth
    Config -. configuracion .-> Product
    Config -. configuracion .-> Pricing
    Config -. configuracion .-> Policy
    Config -. configuracion .-> Report
    Config --> ConfigGit[(Repositorio Git\nMicroservices.Demo-config)]

    Discovery[Discovery Server\nEureka\n:8761] -. registro y descubrimiento .-> Gateway
    Discovery -. registro y descubrimiento .-> Auth
    Discovery -. registro y descubrimiento .-> Product
    Discovery -. registro y descubrimiento .-> Pricing
    Discovery -. registro y descubrimiento .-> Policy
    Discovery -. registro y descubrimiento .-> Report

    Auth -. logs .-> Logstash[Logstash\nHTTP :28080]
    Product -. logs .-> Logstash
    Pricing -. logs .-> Logstash
    Policy -. logs .-> Logstash
    Report -. logs .-> Logstash
    Gateway -. logs .-> Logstash
    Logstash --> Logs[(Elasticsearch\nms-services-logs\n:9200)]
    Logs --> Kibana[Kibana\n:5601]

    Auth -. APM .-> APM[APM Server\n:8200]
    Product -. APM .-> APM
    Pricing -. APM .-> APM
    Policy -. APM .-> APM
    Report -. APM .-> APM
    APM --> Logs

    Auth -. trazas .-> Jaeger[Jaeger\n:16686]
    Product -. trazas .-> Jaeger
    Pricing -. trazas .-> Jaeger
    Policy -. trazas .-> Jaeger
    Report -. trazas .-> Jaeger
```

## Diagrama de secuencia: flujo completo

El siguiente flujo muestra una sesión típica desde que el navegador carga la aplicación hasta la creación de una póliza. Las llamadas de configuración, descubrimiento y observabilidad ocurren alrededor de las llamadas de negocio.

```mermaid
sequenceDiagram
    autonumber
    actor Usuario
    participant Web as Client Web Angular
    participant Gateway as API Gateway / Ocelot
    participant Config as Config Server
    participant Eureka as Discovery Server
    participant Auth as Auth API
    participant Product as Product API
    participant Pricing as Pricing API
    participant Policy as Policy API
    participant ProductDb as Product DB
    participant PolicyDb as Policy DB
    participant Rabbit as RabbitMQ
    participant Logstash
    participant Elastic as Elasticsearch
    participant APM as APM Server
    participant Jaeger

    Usuario->>Web: Abre http://localhost:8081
    Web->>Gateway: Solicita datos de la aplicación
    Gateway->>Config: Carga rutas y propiedades
    Config->>Config: Lee Microservices.Demo-config desde Git
    Config-->>Gateway: Devuelve configuración Ocelot

    par Registro de servicios
        Auth->>Config: Carga configuración
        Product->>Config: Carga configuración
        Pricing->>Config: Carga configuración
        Policy->>Config: Carga configuración
        Auth->>Eureka: Se registra
        Product->>Eureka: Se registra
        Pricing->>Eureka: Se registra
        Policy->>Eureka: Se registra
    end

    Usuario->>Web: Introduce usuario y contraseña
    Web->>Gateway: POST /api/users
    Gateway->>Eureka: Localiza Auth API
    Gateway->>Auth: Reenvía credenciales
    Auth->>Auth: Valida usuario y genera JWT
    Auth->>Logstash: Envía log estructurado
    Auth->>APM: Envía telemetría
    Auth-->>Gateway: Devuelve JWT
    Gateway-->>Web: Respuesta de autenticación

    Usuario->>Web: Consulta productos
    Web->>Gateway: GET /api/products\nAuthorization: Bearer JWT
    Gateway->>Eureka: Localiza Product API
    Gateway->>Product: Reenvía solicitud autenticada
    Product->>ProductDb: Consulta catálogo
    Product-->>Gateway: Devuelve productos
    Product->>Logstash: Envía log estructurado
    Product->>APM: Envía telemetría
    Gateway-->>Web: Lista de productos

    Usuario->>Web: Solicita una oferta
    Web->>Gateway: POST /api/offers\nAuthorization: Bearer JWT
    Gateway->>Eureka: Localiza Policy API
    Gateway->>Policy: Reenvía oferta
    Policy->>Eureka: Localiza Pricing API
    Policy->>Pricing: Solicita cálculo de precio
    Pricing-->>Policy: Devuelve precio y coberturas
    Policy->>PolicyDb: Persiste la oferta
    Policy-->>Gateway: Devuelve número de oferta
    Gateway-->>Web: Muestra la oferta

    Usuario->>Web: Confirma la oferta
    Web->>Gateway: POST /api/policies\nOfferNumber + datos del titular
    Gateway->>Eureka: Localiza Policy API
    Gateway->>Policy: Reenvía creación de póliza
    Policy->>PolicyDb: Busca y actualiza la oferta
    Policy->>PolicyDb: Guarda la póliza
    Policy->>Rabbit: Publica PolicyCreated
    Policy->>Logstash: Envía log estructurado
    Policy->>APM: Envía telemetría
    Policy->>Jaeger: Envía spans de la petición
    Gateway-->>Web: Devuelve número de póliza
    Web-->>Usuario: Muestra confirmación

    par Persistencia de observabilidad
        Logstash->>Elastic: Escribe en ms-services-logs
        APM->>Elastic: Escribe índices apm-*
        Jaeger->>Jaeger: Almacena y agrupa trazas
    end
```

## Responsabilidades

| Componente       | Responsabilidad                                                  |                       Puerto local |
| ---------------- | ---------------------------------------------------------------- | ---------------------------------: |
| Client Web       | Interfaz Angular para autenticación y operaciones de negocio.    |                             `8081` |
| API Gateway      | Entrada HTTP pública, rutas Ocelot, JWT y descubrimiento Eureka. |                            `44399` |
| Auth API         | Valida usuarios y emite tokens JWT.                              |                       interno `80` |
| Product API      | Administra el catálogo y el estado de los productos.             |                       interno `80` |
| Pricing API      | Calcula precios y persiste tarifas con Marten.                   |                       interno `80` |
| Policy API       | Crea, consulta y termina pólizas; usa Pricing y RabbitMQ.        |                       interno `80` |
| Report API       | Compone reportes consultando Product y Policy.                   |                       interno `80` |
| Config Server    | Distribuye configuración centralizada desde Git.                 |                             `8889` |
| Discovery Server | Registra servicios y localiza instancias mediante Eureka.        |                             `8761` |
| Bases de datos   | Persistencia aislada por bounded context.                        | `27018`, `14331`, `14332`, `54321` |
| RabbitMQ         | Mensajería de eventos de Policy API.                             |                    `5672`, `15672` |
| Logstash         | Recibe y transforma logs HTTP.                                   |                            `28080` |
| Elasticsearch    | Indexa logs en `ms-services-logs` y datos APM.                   |                             `9200` |
| Kibana           | Consulta logs y crea visualizaciones.                            |                             `5601` |
| APM Server       | Recibe telemetría de rendimiento y errores.                      |                             `8200` |
| Jaeger           | Visualiza trazas distribuidas.                                   |                            `16686` |

## Flujos principales

### Autenticación

1. El cliente envía credenciales a `POST /api/users` mediante el Gateway.
2. Auth API consulta MongoDB.
3. Auth API devuelve un JWT.
4. El cliente usa `Authorization: Bearer <token>` en las operaciones protegidas.

### Creación de una póliza

1. El cliente envía `POST /api/policies` mediante el Gateway.
2. Policy API consulta la oferta en SQL Server.
3. Policy API puede consultar Pricing API mediante Eureka.
4. Policy API persiste la póliza.
5. Policy API publica eventos en RabbitMQ.

### Observabilidad

1. Los servicios envían logs estructurados a Logstash por HTTP.
2. Logstash escribe los documentos en Elasticsearch, índice `ms-services-logs`.
3. En Kibana, la Data View recomendada es exactamente `ms-services-logs` con `@timestamp`.
4. APM Server almacena telemetría en índices `apm-*` y Jaeger muestra las trazas distribuidas.

## Compose

- `docker-compose-infr.yml`: Config Server, Discovery Server, Gateway, Auth, RabbitMQ y observabilidad.
- `docker-compose-db.yml`: MongoDB, SQL Server y PostgreSQL.
- `docker-compose-app.yml`: APIs de dominio y cliente Angular.
- `docker-compose-all.yml`: infraestructura, APIs y cliente, sin bases de datos.
