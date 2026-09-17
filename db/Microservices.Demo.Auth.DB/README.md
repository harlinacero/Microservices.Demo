# Microservices.Demo.Auth.DB

Base MongoDB del servicio de autenticación.

- Imagen: `mongo:latest`.
- Puerto local: `27018` hacia el puerto interno `27017`.
- Base: `SecurityDB`.
- Colección: `Users`.
- Inicialización: `Docker/init.js`.

El script crea tres usuarios demo: `erick/erick`, `eva/eva` y `oscar/oscar`, junto con nombre, avatar y productos disponibles. Auth API usa `SecurityDatabaseSettings` para localizar esta base y colección.

```powershell
docker-compose -f ..\..\docker-compose-db.yml build microservices.demo.auth.db
docker-compose -f ..\..\docker-compose-db.yml up -d microservices.demo.auth.db
```

No usar las credenciales incluidas fuera de un entorno de práctica.
