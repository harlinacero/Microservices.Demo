CREATE TABLE [dbo].[Policy]
(
	PolicyId INT NOT NULL PRIMARY KEY IDENTITY,
	Number VARCHAR(250),
	ProductCode VARCHAR(250),
	AgentLogin VARCHAR(250),
	PolicyStatusId INT NOT NULL,
	CreationDate DATETIME2,

	CONSTRAINT FK_PolicyPolicyStatus FOREIGN KEY (PolicyStatusId) REFERENCES PolicyStatus(PolicyStatusId),
)
