using Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Dtos.Product;
using Microservices.Demo.Product.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Queries.Infrastructure.Adapters
{
    public static class ProductAdapter
    {
        public static IList<CoverDto> FromCoversToCoverDtos(IList<Cover> covers)
        {
            return covers?.Select(c => FromCoverToCoverDto(c)).ToList();
        }

        public static IList<QuestionDto> FromQuestionsToQuestionDtos(IList<Question> questions)
        {
            return questions?.Select(q => FromQuestionToQuestionDto(q)).ToList();
        }

        private static CoverDto FromCoverToCoverDto(Cover cover)
        {
            return new CoverDto
            {
                Code = cover.Code,
                Name = cover.Name,
                Description = cover.Description,
                Optional = cover.Optional,
                SumInsured = cover.SumInsured
            };
        }

        private static QuestionDto FromQuestionToQuestionDto(Question question)
        {
            switch (question.GetType().Name)
            {
                case "NumericQuestion":
                    return new NumericQuestionDto { QuestionCode = question.Code, Index = question.Index, Text = question.Text };
                case "ChoiceQuestion":
                    return new ChoiceQuestionDto
                    {
                        QuestionCode = question.Code,
                        Index = question.Index,
                        Text = question.Text,
                        Choices = ((ChoiceQuestion)question).Choices?.Select(c => new ChoiceDto { Code = c.Code, Label = c.Label }).ToList()
                    };
                case "DateQuestion":
                    return new DateQuestionDto { QuestionCode = question.Code, Index = question.Index, Text = question.Text };

                default:
                    throw new ArgumentOutOfRangeException(question.GetType().Name);
            }
        }
    }
}
