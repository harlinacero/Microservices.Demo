# Microservices.Demo.Pricing.DB

Base PostgreSQL del servicio de precios.

Este proyecto proporciona el almacenamiento de `Microservices.Demo.Pricing.API`. Marten usa esta base para documentos y estructuras de precios; Policy API la consume a través de Pricing API, nunca mediante una conexión directa.

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

La conexión desde el host usa `localhost:54321`; desde otro contenedor de la red `backend` usa `microservices.demo.pricing.db:5432`.

La imagen usa una etiqueta móvil (`latest`); para un entorno reproducible conviene fijar una versión concreta.
