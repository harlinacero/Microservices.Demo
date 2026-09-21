# Microservices.Demo.Product.DB

Base SQL Server 2019 del catálogo de productos.

Este proyecto contiene la base exclusiva de `Microservices.Demo.Product.API`. Guarda el catálogo y el estado de los productos; Policy API y Report API acceden a esa información mediante Product API.

- Puerto local: `14331` hacia `1433`.
- Contraseña SA de demo: `Password1234`.
- Scripts Docker: `entrypoint.sh`, `SqlCmdStartup.sh` y `SqlCmdScript.sql`.
- Proyecto SSDT: `Microservices.Demo.Product.DB.sqlproj`.

El entrypoint inicializa SQL Server y ejecuta el script de esquema/datos antes de que Product API consulte la base. Los datos se conservan en `Docker/Data` mediante el volumen de Compose.

```powershell
docker-compose -f ..\..\docker-compose-db.yml build microservices.demo.product.db
docker-compose -f ..\..\docker-compose-db.yml up -d microservices.demo.product.db
```

La conexión desde el host usa `localhost,14331`; desde otro contenedor de la red `backend` usa `microservices.demo.product.db,1433`.
