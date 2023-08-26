FROM mcr.microsoft.com/mssql/server:2019-latest AS sqlserver
ENV SA_PASSWORD=Password1234
ENV ACCEPT_EULA=Y
COPY ["db/Microservices.Demo.Policy.DB/Docker/entrypoint.sh", "/var/opt/sqlserver/entrypoint.sh"]
COPY ["db/Microservices.Demo.Policy.DB/Docker/SqlCmdStartup.sh", "/var/opt/sqlserver/SqlCmdStartup.sh"]
COPY ["db/Microservices.Demo.Policy.DB/Docker/SqlCmdScript.sql", "/var/opt/sqlserver/SqlCmdScript.sql"]
USER root
RUN  chmod +x /var/opt/sqlserver/SqlCmdStartup.sh
USER mssql
CMD /bin/bash /var/opt/sqlserver/entrypoint.sh
