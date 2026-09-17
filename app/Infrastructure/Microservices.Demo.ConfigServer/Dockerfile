FROM eclipse-temurin:17-jre-jammy AS base
WORKDIR /app

FROM maven:3.8.4-openjdk-17 AS build
WORKDIR /build

ARG OTEL_JAVA_AGENT_VERSION=1.31.0
RUN curl -fsSL "https://repo1.maven.org/maven2/io/opentelemetry/javaagent/opentelemetry-javaagent/${OTEL_JAVA_AGENT_VERSION}/opentelemetry-javaagent-${OTEL_JAVA_AGENT_VERSION}.jar" -o opentelemetry-javaagent.jar

COPY pom.xml ./
RUN mvn dependency:go-offline

FROM build AS publish
COPY src ./src
RUN mvn -f pom.xml clean package -DskipTests

FROM base AS final
WORKDIR /app
COPY --from=publish /build/target/*.jar ./app.jar
COPY --from=publish /build/opentelemetry-javaagent.jar ./opentelemetry-javaagent.jar

ENTRYPOINT ["java","-javaagent:opentelemetry-javaagent.jar", "-jar", "/app/app.jar"]