using Microservices.Demo.Product.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.Infrastructure.Data.Entities
{
    public partial class Question
    {
        protected Question(string code, int index, string text)
        {
            Code = code;
            Index = index;
            Text = text;
        }
    }
    public class NumericQuestion : Question
    {
        public NumericQuestion(string code, int index, string text) : base(code, index, text)
        { }
    }

    public class DateQuestion : Question
    {
        public DateQuestion(string code, int index, string text) : base(code, index, text)
        { }
    }

    public class ChoiceQuestion : Question
    {
        public ChoiceQuestion()
        { }
        public ChoiceQuestion(string code, int index, string text, List<Choice> choices) : base(code, index, text)
        {
            Choices = choices;
        }

        public static List<Choice> YesNoChoice()
        {
            return new List<Choice> {
                    new Choice("YES", "Yes"),
                    new Choice("NO", "No")
            };
        }
    }
}
