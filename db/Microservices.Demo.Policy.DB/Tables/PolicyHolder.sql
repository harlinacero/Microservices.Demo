CREATE TABLE [dbo].[PolicyHolder]
(
	PolicyHolderId INT NOT NULL PRIMARY KEY IDENTITY,
	FirstName VARCHAR(250),
	LastName VARCHAR(250),
	Pesel VARCHAR(250),
	AddressId INT NOT NULL,

	CONSTRAINT FK_PolicyHolderAddress FOREIGN KEY (AddressId) REFERENCES Address(AddressId),
)
