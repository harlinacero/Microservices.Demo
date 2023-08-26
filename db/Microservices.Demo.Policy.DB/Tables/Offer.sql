CREATE TABLE [dbo].[Offer]
(
	OfferId INT NOT NULL PRIMARY KEY IDENTITY,
	Number VARCHAR(250),
	ProductCode VARCHAR(250),
	TotalPrice DECIMAL NOT NULL,
	CreationDate DATETIME2 NOT NULL,
	AgentLogin VARCHAR(250),
	PolicyValidityPeriodId INT NOT NULL,
	PolicyHolderId INT NULL,
	OfferStatusId INT NOT NULL,
	
	CONSTRAINT FK_OfferPolicyValidityPeriod FOREIGN KEY (PolicyValidityPeriodId) REFERENCES PolicyValidityPeriod(PolicyValidityPeriodId),
	CONSTRAINT FK_OfferPolicyHolder FOREIGN KEY (PolicyHolderId) REFERENCES PolicyHolder(PolicyHolderId),
	CONSTRAINT FK_OfferOfferStatus FOREIGN KEY (OfferStatusId) REFERENCES OfferStatus(OfferStatusId),
)
