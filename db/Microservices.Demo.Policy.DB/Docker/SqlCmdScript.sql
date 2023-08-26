SELECT @@VERSION;

CREATE DATABASE [Microservices.Demo.Policy]
ON	(FILENAME = '/var/opt/mssql/Microservices.Demo.Policy/data/Microservices.Demo.Policy.mdf'),
	(FILENAME = '/var/opt/mssql/Microservices.Demo.Policy/data/Microservices.Demo.Policy_log.ldf') 
FOR ATTACH