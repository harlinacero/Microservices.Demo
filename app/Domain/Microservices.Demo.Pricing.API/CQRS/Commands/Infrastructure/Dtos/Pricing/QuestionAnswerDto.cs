using Microservices.Demo.Pricing.API.CQRS.Commands.Infrastructure.Dtos.Converters;
using Microservices.Demo.Pricing.API.Infrastructure.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Pricing.API.CQRS.Commands.Infrastructure.Dtos.Pricing
{
    [JsonConverter(typeof(QuestionAnswerDtoConverter))]
    public abstract class QuestionAnswerDto
    {
        public string QuestionCode { get; set; }
        public abstract QuestionType QuestionType { get; }
        public abstract object GetAnswer();

    }
    public abstract class QuestionAnswerDto<T> : QuestionAnswerDto
    {
        public T Answer { get; set; }

        public override object GetAnswer() => Answer;
    }

    public class TextQuestionAnswer : QuestionAnswerDto<string>
    {
        public override QuestionType QuestionType => QuestionType.Text;
    }


    public class NumericQuestionAnswer : QuestionAnswerDto<decimal>
    {
        public override QuestionType QuestionType => QuestionType.Numeric;
    }

    public class ChoiceQuestionAnswer : QuestionAnswerDto<string>
    {
        public override QuestionType QuestionType => QuestionType.Choice;
    }
}
