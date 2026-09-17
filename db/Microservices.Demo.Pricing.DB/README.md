# Microservices.Demo.Pricing.DB

Base PostgreSQL del servicio de precios.

- Imagen: `postgres:latest`.
- Puerto local: `54321` hacia `5432`.
- Contraseña de demo: `Password1234`.
- Inicialización: `Docker/init.sql`.
- Base creada: `Microservices.Demo.Pricing`.

Pricing API usa Marten para persistir y consultar sus documentos/estructuras de precios.

```powershell
docker-compose -f ..\..\docker-compose-db.yml build microservices.demo.pricing.db
docker-compose -f ..\..\docker-compose-db.yml up -d microservices.demo.pricing.db
```

La imagen usa una etiqueta móvil (`latest`); para un entorno reproducible conviene fijar una versión concreta.
