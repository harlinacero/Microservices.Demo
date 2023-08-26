CREATE TABLE [dbo].[Question]
(
	QuestionId INT NOT NULL PRIMARY KEY IDENTITY,
	Code VARCHAR(255) NOT NULL,
	[Index] INT NOT NULL,
	Text VARCHAR(255),
	ProductId INT NOT NULL,
	QuestionTypeId INT NOT NULL

	CONSTRAINT FK_QuestionProduct FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
	CONSTRAINT FK_QuestionQuestionType FOREIGN KEY (QuestionTypeId) REFERENCES QuestionType(QuestionTypeId)
)
