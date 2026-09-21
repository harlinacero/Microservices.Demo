# Microservices.Demo.Auth.DB

Base MongoDB del servicio de autenticación.

Este proyecto proporciona la persistencia de `Microservices.Demo.Auth.API`; no es un API independiente. Guarda los usuarios que Auth API valida y la información que incluye en los tokens JWT.

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

Para inspeccionarla desde el equipo host:

```powershell
mongosh mongodb://localhost:27018/SecurityDB
```

No usar las credenciales incluidas fuera de un entorno de práctica.
