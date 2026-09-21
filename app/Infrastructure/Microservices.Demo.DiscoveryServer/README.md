# Microservices.Demo.DiscoveryServer

Servidor Eureka de Spring Cloud. Funciona como una agenda dinámica de servicios: cada API publica su nombre y dirección al iniciar, y los consumidores preguntan a Eureka dónde encontrarlo.

## Qué resuelve

- Evita configurar IPs de contenedores que pueden cambiar.
- Permite varias instancias del mismo API.
- Ayuda al Gateway y a los clientes internos a elegir un destino.
- Muestra el estado de registro de los servicios.

## Configuración

- Puerto: `8761`.
- Nombre: `Microservices.Demo.DiscoveryServer`.
- Entrada Java: `src/main/java/io/microservices/discoveryserver/DiscoveryServerApplication.java`.

Los servicios .NET se registran con nombres como `Microservices.Demo.Auth.API`, `Microservices.Demo.Product.API` y `Microservices.Demo.Policy.API`. El Gateway consulta este registro mediante Ocelot Provider Eureka.

## Flujo de descubrimiento

```text
API inicia -> se registra en Eureka -> consumidor consulta el nombre -> Eureka devuelve una instancia
```

Eureka no enruta ni procesa las peticiones de negocio. Solo mantiene el registro; el Gateway o el cliente HTTP realizan la llamada posterior.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml build microservices.demo.discoveryserver
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.discoveryserver
```

La consola está en `http://localhost:8761`. Si un servicio no aparece, revisar su `appsettings.json`, el nombre Eureka y los logs del servicio.

Una ausencia temporal durante el arranque puede ser normal. Si persiste, comprueba que el servicio usa el mismo nombre que el `ServiceName` de la ruta Ocelot y que apunta a `http://microservices.demo.discoveryserver:8761/eureka`.
