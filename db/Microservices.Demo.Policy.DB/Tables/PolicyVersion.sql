CREATE TABLE [dbo].[PolicyVersion]
(
	PolicyVersionId INT NOT NULL PRIMARY KEY IDENTITY,
	VersionNumber INT NOT NULL,
	TotalPremiumAmount DECIMAL NOT NULL,
	PolicyId INT NOT NULL,
	PolicyHolderId INT NOT NULL,
	CoverPeriodPolicyValidityPeriodId INT NOT NULL,
	VersionValidityPeriodPolicyValidityPeriodId INT NOT NULL,

	CONSTRAINT FK_PolicyVersionPolicy FOREIGN KEY (PolicyId) REFERENCES [Policy](PolicyId),
	CONSTRAINT FK_PolicyVersionPolicyHolder FOREIGN KEY (PolicyHolderId) REFERENCES PolicyHolder(PolicyHolderId),
	CONSTRAINT FK_PolicyVersionCoverPeriodPolicyValidityPeriod FOREIGN KEY (CoverPeriodPolicyValidityPeriodId) REFERENCES PolicyValidityPeriod(PolicyValidityPeriodId),
	CONSTRAINT FK_PolicyVersionVersionValidityPeriodPolicyValidityPeriod FOREIGN KEY (VersionValidityPeriodPolicyValidityPeriodId) REFERENCES PolicyValidityPeriod(PolicyValidityPeriodId),
)
