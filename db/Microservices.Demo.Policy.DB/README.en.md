# Microservices.Demo.Policy.DB

SQL Server 2019 database for the policy service.

This project contains the database owned exclusively by `Microservices.Demo.Policy.API`. It stores policies and offers; Pricing API and Product API have their own databases and must not query these tables directly.

- Local port: `14332` mapped to `1433`.
- Demo SA password: `Password1234`.
- Docker scripts: `entrypoint.sh`, `SqlCmdStartup.sh`, and `SqlCmdScript.sql`.
- SSDT project: `Microservices.Demo.Policy.DB.sqlproj`.

The entrypoint waits for SQL Server, runs the preparation script, and makes the tables and seed data required by Policy API available. Data is kept in `Docker/Data` through the volume defined in Compose.

```powershell
docker-compose -f ..\..\docker-compose-db.yml build microservices.demo.policy.db
docker-compose -f ..\..\docker-compose-db.yml up -d microservices.demo.policy.db
```

From the host use `localhost,14332`; from another container on `backend` use `microservices.demo.policy.db,1433`.
