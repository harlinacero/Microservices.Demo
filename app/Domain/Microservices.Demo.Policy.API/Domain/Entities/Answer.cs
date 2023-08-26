using Microservices.Demo.Policy.API.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Data.Entities
{
    public abstract class Answer
    {
        public string QuestionCode { get; protected set; }
        public abstract object GetAnswerValue();
        public static Answer Create(QuestionType questionType, string questionCode, object answerValue)
        {
            switch (questionType)
            {
                case QuestionType.Text:
                    return new TextAnswer(questionCode, (string)answerValue);
                case QuestionType.Choice:
                    return new ChoiceAnswer(questionCode, (string)answerValue);
                case QuestionType.Numeric:
                    return new NumericAnswer(questionCode, (decimal)answerValue);
                default:
                    throw new ArgumentException();
            }
        }
    }

    public abstract class Answer<T> : Answer
    {
        public T AnswerValue { get; protected set; }

        public override object GetAnswerValue() => AnswerValue;        
    }

    public class TextAnswer : Answer<string>
    {
        public TextAnswer() { } 

        public TextAnswer(string questionCode, string answer)
        {
            this.QuestionCode = questionCode;
            this.AnswerValue = answer;
        }
    }

    public class NumericAnswer : Answer<decimal>
    {
        public NumericAnswer() { }

        public NumericAnswer(string questionCode, decimal answer)
        {
            this.QuestionCode = questionCode;
            this.AnswerValue = answer;
        }
    }

    public class ChoiceAnswer : Answer<string>
    {
        public ChoiceAnswer() { }

        public ChoiceAnswer(string questionCode, string answer)
        {
            this.QuestionCode = questionCode;
            this.AnswerValue = answer;
        }
    }
}
