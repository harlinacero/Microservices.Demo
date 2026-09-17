# Microservices.Demo.Client.Web

Cliente web Angular 14 para la demo. Presenta la autenticación y las operaciones de productos, precios, pólizas y reportes. Sus servicios HTTP llaman al API Gateway, no directamente a cada microservicio.

## Ejecución

En desarrollo:

```powershell
npm install
npm start
```

La aplicación queda en `http://localhost:4200`. En Docker se sirve mediante Nginx en `http://localhost:8081`.

```powershell
docker-compose -f ..\..\..\docker-compose-app.yml build microservices.demo.client.web
docker-compose -f ..\..\..\docker-compose-app.yml up -d microservices.demo.client.web
```

El Gateway debe estar disponible en `http://localhost:44399` y las bases y APIs deben estar levantadas.

## Estructura funcional

- `src/app/services`: clientes HTTP para autenticación y los dominios de negocio.
- `src/app/guards`: protección de rutas según el estado de autenticación.
- `src/assets`: imágenes y recursos estáticos.
- `src/environments`: configuración de compilación Angular.

## Comandos

```powershell
npm start
npm run build
npm test -- --watch=false --browsers=ChromeHeadless
```

Si el navegador muestra errores de red, comprobar primero el Gateway en `http://localhost:44399` y revisar `docker logs Microservices.Demo.Client.Web.ApiGateway`.
