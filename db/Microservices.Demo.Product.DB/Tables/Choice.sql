CREATE TABLE [dbo].[Choice]
(
	ChoiceId INT NOT NULL PRIMARY KEY IDENTITY,
	Code VARCHAR(255),
	Label VARCHAR(255),
	QuestionId INT NOT NULL

	CONSTRAINT FK_ChoiceQuestion FOREIGN KEY (QuestionId) REFERENCES Question(QuestionId)
)
