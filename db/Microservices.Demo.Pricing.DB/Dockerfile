FROM postgres:latest AS postgres
ENV POSTGRES_PASSWORD=Password1234
COPY ["db/Microservices.Demo.Pricing.DB/Docker/init.sql", "/docker-entrypoint-initdb.d/init.sql"]
