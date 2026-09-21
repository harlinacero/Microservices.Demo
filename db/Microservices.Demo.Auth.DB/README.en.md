# Microservices.Demo.Auth.DB

MongoDB database for the authentication service.

This project provides persistence for `Microservices.Demo.Auth.API`; it is not an independent API. It stores the users that Auth API validates and the information included in JWT claims.

- Image: `mongo:latest`.
- Local port: `27018` mapped to internal port `27017`.
- Database: `SecurityDB`.
- Collection: `Users`.
- Initialization: `Docker/init.js`.

The script creates three demo users: `erick/erick`, `eva/eva`, and `oscar/oscar`, together with names, avatars, and available products. Auth API uses `SecurityDatabaseSettings` to locate this database and collection.

```powershell
docker-compose -f ..\..\docker-compose-db.yml build microservices.demo.auth.db
docker-compose -f ..\..\docker-compose-db.yml up -d microservices.demo.auth.db
```

To inspect it from the host:

```powershell
mongosh mongodb://localhost:27018/SecurityDB
```

Do not use the included credentials outside a practice environment.
