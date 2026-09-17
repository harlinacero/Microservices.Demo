# Microservices.Demo.Policy.DB

Base SQL Server 2019 del servicio de pólizas.

- Puerto local: `14332` hacia `1433`.
- Contraseña SA de demo: `Password1234`.
- Scripts Docker: `entrypoint.sh`, `SqlCmdStartup.sh` y `SqlCmdScript.sql`.
- Proyecto SSDT: `Microservices.Demo.Policy.DB.sqlproj`.

El entrypoint espera a SQL Server, ejecuta el script de preparación y deja disponibles las tablas y datos necesarios por Policy API. Los datos se conservan en `Docker/Data` mediante el volumen definido en Compose.

```powershell
docker-compose -f ..\..\docker-compose-db.yml build microservices.demo.policy.db
docker-compose -f ..\..\docker-compose-db.yml up -d microservices.demo.policy.db
```
