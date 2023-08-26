CREATE TABLE [dbo].[PolicyCover]
(
	PolicyCoverId INT NOT NULL PRIMARY KEY IDENTITY,
	Code VARCHAR(250),
	Premium DECIMAL NOT NULL,
	PolicyValidityPeriodId INT NOT NULL,
	PolicyVersionId INT NOT NULL,

	CONSTRAINT FK_PolicyCoverPolicyValidityPeriod FOREIGN KEY (PolicyValidityPeriodId) REFERENCES PolicyValidityPeriod(PolicyValidityPeriodId),
	CONSTRAINT FK_PolicyCoverPolicyVersion FOREIGN KEY (PolicyVersionId) REFERENCES PolicyVersion(PolicyVersionId),
)
