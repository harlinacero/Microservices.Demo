# Microservices.Demo.DiscoveryServer

Servidor Eureka de Spring Cloud. Mantiene el registro de instancias y permite que Ocelot y los APIs se encuentren por nombre sin fijar IPs o puertos efímeros.

## Configuración

- Puerto: `8761`.
- Nombre: `Microservices.Demo.DiscoveryServer`.
- Entrada Java: `src/main/java/io/microservices/discoveryserver/DiscoveryServerApplication.java`.

Los servicios .NET se registran con nombres como `Microservices.Demo.Auth.API`, `Microservices.Demo.Product.API` y `Microservices.Demo.Policy.API`. El Gateway consulta este registro mediante Ocelot Provider Eureka.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-infr.yml build microservices.demo.discoveryserver
docker-compose -f ..\..\..\docker-compose-infr.yml up -d microservices.demo.discoveryserver
```

La consola está en `http://localhost:8761`. Si un servicio no aparece, revisar su `appsettings.json`, el nombre Eureka y los logs del servicio.
