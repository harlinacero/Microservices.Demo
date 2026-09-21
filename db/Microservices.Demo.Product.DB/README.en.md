# Microservices.Demo.Product.DB

SQL Server 2019 database for the product catalog.

This project contains the database owned exclusively by `Microservices.Demo.Product.API`. It stores the catalog and product state; Policy API and Report API access that information through Product API.

- Local port: `14331` mapped to `1433`.
- Demo SA password: `Password1234`.
- Docker scripts: `entrypoint.sh`, `SqlCmdStartup.sh`, and `SqlCmdScript.sql`.
- SSDT project: `Microservices.Demo.Product.DB.sqlproj`.

The entrypoint initializes SQL Server and runs the schema/data script before Product API queries the database. Data is kept in `Docker/Data` through the Compose volume.

```powershell
docker-compose -f ..\..\docker-compose-db.yml build microservices.demo.product.db
docker-compose -f ..\..\docker-compose-db.yml up -d microservices.demo.product.db
```

From the host use `localhost,14331`; from another container on `backend` use `microservices.demo.product.db,1433`.
