# Microservices.Demo.Product.API

Microservicio de catálogo de productos. Expone la información y el ciclo de vida de los productos que luego consumen Policy, Report y el cliente web.

## Tecnología

ASP.NET Core/.NET 6, EF Core, SQL Server, MediatR y Steeltoe para Config Server/Eureka. El punto de entrada es `Program.cs`; los controladores están en `Controllers` y la lógica se separa en `Application`, `CQRS`, `Domain` e `Infrastructure`.

## Endpoints

La ruta base es `/api/products`:

- `GET /api/products`: lista productos.
- `GET /api/products/{code}`: obtiene un producto por código.
- `POST /api/products`: crea un producto.
- `POST /activate`: activa un producto.
- `POST /discontinue`: descontinúa un producto.

Las rutas protegidas requieren el JWT emitido por Auth API cuando se accede mediante Gateway.

## Dependencias

- SQL Server `microservices.demo.product.db`, puerto local `14331`.
- Config Server `microservices.demo.configserver:8889`.
- Eureka `microservices.demo.discoveryserver:8761`.
- Logstash, Jaeger y APM para observabilidad.

El servicio se registra en Eureka como `Microservices.Demo.Product.API`. El Compose puede levantar varias réplicas; sus puertos internos son `80` y se recomienda acceder por el Gateway.

## Docker

```powershell
docker-compose -f ..\..\..\docker-compose-all.yml build microservices.demo.product.api
docker-compose -f ..\..\..\docker-compose-all.yml up -d microservices.demo.product.api
```

La base debe estar disponible antes de iniciar el API.

## Ejecución local

Requiere el SDK de .NET 6. Arranca primero Product DB, Config Server y Eureka; después ejecuta desde esta carpeta:

```powershell
dotnet restore
dotnet run
```

El API usa la configuración externa para conectarse a SQL Server y registrarse en Eureka. Para una prueba completa, accede a sus rutas mediante el Gateway en `http://localhost:44399`.
