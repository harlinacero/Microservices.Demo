# Wait for SQL Server to come up. The current image ships sqlcmd under
# mssql-tools18.
sleep 90s

# Fail the setup process if the database attach or schema script fails.
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -d master -C -b -i /var/opt/sqlserver/SqlCmdScript.sql