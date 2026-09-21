# Microservices.Demo.Database

Este directorio contiene los proyectos de persistencia de la plataforma Microservices.Demo. Cada bounded context tiene una base separada para evitar que un servicio dependa directamente de las tablas o colecciones de otro.

Las bases se ejecutan como contenedores Docker y se conectan a la red `backend`; las APIs usan los nombres de servicio de Compose como host. Este directorio no contiene lógica de negocio de los APIs.

## Servicios

| Proyecto                        | Motor           | Puerto local | Uso                     |
| ------------------------------- | --------------- | -----------: | ----------------------- |
| `Microservices.Demo.Auth.DB`    | MongoDB         |      `27018` | Usuarios y credenciales |
| `Microservices.Demo.Policy.DB`  | SQL Server 2019 |      `14332` | Pólizas                 |
| `Microservices.Demo.Pricing.DB` | PostgreSQL      |      `54321` | Precios                 |
| `Microservices.Demo.Product.DB` | SQL Server 2019 |      `14331` | Productos               |

## Arranque

```powershell
docker-compose -f ..\docker-compose-db.yml build
docker-compose -f ..\docker-compose-db.yml up -d
```

Para detenerlas sin eliminar los datos:

```powershell
docker-compose -f ..\docker-compose-db.yml stop
```

## Inicialización

- Auth copia `Docker/init.js` al directorio de inicialización de Mongo y crea `SecurityDB.Users` con usuarios demo.
- Pricing copia `Docker/init.sql` a PostgreSQL y crea `Microservices.Demo.Pricing`; Marten crea/usa sus estructuras al iniciar el API.
- Policy y Product usan scripts SQL Server (`entrypoint.sh`, `SqlCmdStartup.sh`, `SqlCmdScript.sql`) para preparar tablas y datos.

Las bases SQL tienen volúmenes para datos de Policy y Product. Mongo y PostgreSQL no montan un volumen explícito en el Compose actual, por lo que sus datos dependen del almacenamiento del contenedor.

## Seguridad

Los Dockerfiles contienen contraseñas demo (`Password1234`) y `init.js` contiene usuarios con contraseñas simples. Son datos educativos, no credenciales para producción.
