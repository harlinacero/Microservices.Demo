INSERT INTO [dbo].[QuestionType]([Description]) 
VALUES
	('Question'),
	('NumericQuestion'),
	('DateQuestion'),
	('ChoiceQuestion');

INSERT INTO [dbo].[ProductStatus]([Description]) 
VALUES
	('Draft'),
	('Active'),
	('Discontinued');


INSERT INTO [dbo].[Product]([Code],[Name],[Image],[Description],[MaxNumberOfInsured],[ProductStatusId])
VALUES
	('TRI','Safe Traveller','/assets/products/travel.jpg','Travel insurance',10,2),
	('HSI','Happy House','/assets/products/house.jpg','House insurance',5,2),
	('FAI','Happy Farm','/assets/products/farm.jpg','Farm insurance',1,2),
	('CAR','Happy Driver','/assets/products/car.jpg','Car insurance',1,2);


INSERT INTO [dbo].[Cover]([Code],[Name],[Description],[Optional],[SumInsured],[ProductId])
VALUES
    ('C2','Illness','',0,5000,1),
	('C3','Assistance','',1,null,1),

	('C1','Fire','',0,200000,2),
	('C2','Flood','',0,100000,2),
	('C3','Theft','',0,50000,2),
	('C4','Assistance','',1,null,2),

	('C1','Crops','',0,200000,3),
	('C2','Flood','',0,100000,3),
	('C3','Fire','',0,50000,3),
	('C4','Equipment','',1,300000,3),

	('C1','Assistance','',1,null,4);

	
INSERT INTO [dbo].[Question]([Code],[Index],[Text],[ProductId],[QuestionTypeId])
VALUES
    ('DESTINATION',1,'Destination',1,4),
	('NUM_OF_ADULTS',2,'Number of adults',1,2),
	('NUM_OF_CHILDREN',3,'Number of children',1,2),

	('TYP',1,'Apartment / House',2,4),
	('AREA',2,'Area',2,2),
	('NUM_OF_CLAIM',3,'Number of claims in last 5 years',2,2),
	('FLOOD',4,'Located in flood risk area',2,4),

	('TYP',1,'Cultivation type',3,4),
	('AREA',2,'Area',3,2),
	('NUM_OF_CLAIM',3,'Number of claims in last 5 years',3,2),

	('NUM_OF_CLAIM',3,'Number of claims in last 5 years',4,2);


INSERT INTO [dbo].[Choice]([Code],[Label],[QuestionId])
VALUES
	('EUR','Europe',1),
	('WORLD','World',1),
	('PL','Poland',1),

	('APT','Apartment',4),
	('HOUSE','House',4),

	('YES','Yes',7),
	('NO','No',7),

	('ZB','Crop',8),
	('KW','Vegetable',8);
