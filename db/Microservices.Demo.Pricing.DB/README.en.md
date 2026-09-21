# Microservices.Demo.Pricing.DB

PostgreSQL database for the pricing service.

This project provides storage for `Microservices.Demo.Pricing.API`. Marten uses this database for pricing documents and structures; Policy API consumes it through Pricing API, never through a direct database connection.

- Image: `postgres:latest`.
- Local port: `54321` mapped to `5432`.
- Demo password: `Password1234`.
- Initialization: `Docker/init.sql`.
- Database created: `Microservices.Demo.Pricing`.

Pricing API uses Marten to persist and query pricing documents and structures.

```powershell
docker-compose -f ..\..\docker-compose-db.yml build microservices.demo.pricing.db
docker-compose -f ..\..\docker-compose-db.yml up -d microservices.demo.pricing.db
```

From the host use `localhost:54321`; from another container on `backend` use `microservices.demo.pricing.db:5432`.

The image uses the moving `latest` tag; pin a concrete version for reproducible environments.
