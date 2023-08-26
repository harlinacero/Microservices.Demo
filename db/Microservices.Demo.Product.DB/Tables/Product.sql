CREATE TABLE [dbo].[Product]
(
	ProductId INT NOT NULL PRIMARY KEY IDENTITY,
	Code VARCHAR(255) NOT NULL,
	Name VARCHAR(255) NOT NULL,
	Image VARCHAR(255),
	Description VARCHAR(255),
	MaxNumberOfInsured INT NOT NULL,
	ProductStatusId INT NOT NULL

	CONSTRAINT FK_ProductProductStatus FOREIGN KEY (ProductStatusId) REFERENCES ProductStatus(ProductStatusId)
)
