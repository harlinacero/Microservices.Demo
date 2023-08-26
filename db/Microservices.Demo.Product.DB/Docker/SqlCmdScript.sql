SELECT @@VERSION;

CREATE DATABASE [Microservices.Demo.Product] 
ON	(FILENAME = '/var/opt/mssql/Microservices.Demo.Product/data/Microservices.Demo.Product.mdf'),
	(FILENAME = '/var/opt/mssql/Microservices.Demo.Product/data/Microservices.Demo.Product_log.ldf') 
FOR ATTACH