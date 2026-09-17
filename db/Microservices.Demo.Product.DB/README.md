# Microservices.Demo.Product.DB

Base SQL Server 2019 del catálogo de productos.

- Puerto local: `14331` hacia `1433`.
- Contraseña SA de demo: `Password1234`.
- Scripts Docker: `entrypoint.sh`, `SqlCmdStartup.sh` y `SqlCmdScript.sql`.
- Proyecto SSDT: `Microservices.Demo.Product.DB.sqlproj`.

El entrypoint inicializa SQL Server y ejecuta el script de esquema/datos antes de que Product API consulte la base. Los datos se conservan en `Docker/Data` mediante el volumen de Compose.

```powershell
docker-compose -f ..\..\docker-compose-db.yml build microservices.demo.product.db
docker-compose -f ..\..\docker-compose-db.yml up -d microservices.demo.product.db
```
