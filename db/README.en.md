# Microservices.Demo.Database

This directory contains the persistence projects for the Microservices.Demo platform. Each bounded context has a separate database so that services do not depend directly on another service's tables or collections.

The databases run as Docker containers on the `backend` network; APIs use Compose service names as internal hosts. This directory contains no business logic.

## Services

| Project                         | Engine          | Local port | Purpose               |
| ------------------------------- | --------------- | ---------: | --------------------- |
| `Microservices.Demo.Auth.DB`    | MongoDB         |    `27018` | Users and credentials |
| `Microservices.Demo.Policy.DB`  | SQL Server 2019 |    `14332` | Policies              |
| `Microservices.Demo.Pricing.DB` | PostgreSQL      |    `54321` | Pricing               |
| `Microservices.Demo.Product.DB` | SQL Server 2019 |    `14331` | Products              |

## Start

```powershell
docker-compose -f ..\docker-compose-db.yml build
docker-compose -f ..\docker-compose-db.yml up -d
```

To stop them without deleting data:

```powershell
docker-compose -f ..\docker-compose-db.yml stop
```

## Initialization

- Auth copies `Docker/init.js` into MongoDB's initialization directory and creates `SecurityDB.Users` with demo users.
- Pricing copies `Docker/init.sql` into PostgreSQL and creates `Microservices.Demo.Pricing`; Marten creates or uses its structures when the API starts.
- Policy and Product use SQL Server scripts (`entrypoint.sh`, `SqlCmdStartup.sh`, and `SqlCmdScript.sql`) to prepare tables and data.

Policy and Product SQL databases use Docker data volumes. MongoDB and PostgreSQL do not mount an explicit volume in the current Compose file, so their data depends on container storage.

## Security

The Dockerfiles contain demo passwords (`Password1234`) and `init.js` contains simple passwords. These values are for educational use only, not production credentials.
