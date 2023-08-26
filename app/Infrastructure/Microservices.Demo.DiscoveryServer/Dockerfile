FROM openjdk:17-slim-buster AS base
WORKDIR /app

FROM maven:3.8.4-openjdk-17 AS build
WORKDIR /build

COPY pom.xml ./
RUN mvn dependency:go-offline

FROM build AS publish
COPY src ./src
RUN mvn -f pom.xml clean package -DskipTests

FROM base AS final
WORKDIR /app
COPY --from=publish /build/target/*.jar ./app.jar
COPY opentelemetry-javaagent.jar ./opentelemetry-javaagent.jar

ENTRYPOINT ["java","-javaagent:opentelemetry-javaagent.jar", "-jar", "/app/app.jar"]

