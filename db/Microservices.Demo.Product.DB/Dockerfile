FROM mcr.microsoft.com/mssql/server:2019-latest AS sqlserver
#FROM microsoft/mssql-server-linux
ENV SA_PASSWORD=Password1234
ENV ACCEPT_EULA=Y
COPY ["db/Microservices.Demo.Product.DB/Docker/entrypoint.sh", "/var/opt/sqlserver/entrypoint.sh"]
COPY ["db/Microservices.Demo.Product.DB/Docker/SqlCmdStartup.sh", "/var/opt/sqlserver/SqlCmdStartup.sh"]
COPY ["db/Microservices.Demo.Product.DB/Docker/SqlCmdScript.sql", "/var/opt/sqlserver/SqlCmdScript.sql"]
USER root
RUN  chmod +x /var/opt/sqlserver/SqlCmdStartup.sh
USER mssql
CMD /bin/bash /var/opt/sqlserver/entrypoint.sh
