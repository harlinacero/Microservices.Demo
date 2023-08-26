#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["app/Domain/Microservices.Demo.Product.API/Microservices.Demo.Product.API.csproj", "app/Domain/Microservices.Demo.Product.API/"]
RUN dotnet restore "app/Domain/Microservices.Demo.Product.API/Microservices.Demo.Product.API.csproj"
COPY . .
WORKDIR "/src/app/Domain/Microservices.Demo.Product.API"

FROM build AS publish
RUN dotnet publish "Microservices.Demo.Product.API.csproj" -c Release -o /app/publish /p:UseAppHost=false
#RUN curl --create-dirs -O --output-dir /elastic/metric https://artifacts.elastic.co/downloads/beats/metricbeat/metricbeat-8.5.2-amd64.deb
#RUN curl --create-dirs -O --output-dir /elastic/heart https://artifacts.elastic.co/downloads/beats/heartbeat/heartbeat-8.5.2-amd64.deb

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

#COPY --from=publish /elastic/metric /elastic/metric
#COPY --from=publish /elastic/heart /elastic/heart
#RUN dpkg -i /elastic/metric/metricbeat-8.5.2-amd64.deb
#RUN dpkg -i /elastic/heart/heartbeat-8.5.2-amd64.deb
#COPY ["app/Domain/Microservices.Demo.Product.API/elastic/metricbeat/metricbeat.yml", "/etc/metricbeat/metricbeat.yml"]
#COPY ["app/Domain/Microservices.Demo.Product.API/elastic/heartbeat/heartbeat.yml", "/etc/heartbeat/heartbeat.yml"]
#RUN metricbeat modules enable docker

#CMD metricbeat setup
#CMD service metricbeat start
#CMD heartbeat setup
#CMD service heartbeat-elastic start

ENTRYPOINT ["dotnet", "Microservices.Demo.Product.API.dll"]