CREATE TABLE [dbo].[PolicyValidityPeriod]
(
	PolicyValidityPeriodId INT NOT NULL PRIMARY KEY IDENTITY,
	PolicyFrom DATETIME2 NOT NULL,
	PolicyTo DATETIME2 NOT NULL
)
