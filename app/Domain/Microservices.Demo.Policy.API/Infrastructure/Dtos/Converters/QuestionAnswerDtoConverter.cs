namespace Microservices.Demo.Policy.API.Infrastructure.Dtos.Converters
{
    using Microservices.Demo.Policy.API.CQRS.Commands.Infrastructure.Dtos.Offer;
    using Microservices.Demo.Policy.API.Infrastructure.Enum;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class QuestionAnswerDtoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsAssignableFrom(typeof(QuestionAnswerDto));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = Create(jsonObject);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is QuestionAnswerDto questionAnswer)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("QuestionCode");
                serializer.Serialize(writer, questionAnswer.QuestionCode);
                writer.WritePropertyName("QuestionType");
                serializer.Serialize(writer, questionAnswer.QuestionType);
                writer.WritePropertyName("Answer");
                serializer.Serialize(writer, questionAnswer.GetAnswer());
                writer.WriteEndObject();
            }

        }

        private static QuestionAnswerDto Create(JObject jsonObject)
        {
            // examine the $type value
            var typeName = Enum.Parse<QuestionType>(jsonObject["QuestionType"].ToString());
            switch (typeName)
            {
                case QuestionType.Text:
                    return new TextQuestionAnswer
                    {
                        QuestionCode = jsonObject["QuestionCode"].ToString(),
                        Answer = jsonObject["Answer"].ToString()
                    };
                case QuestionType.Numeric:
                    return new NumericQuestionAnswer
                    {
                        QuestionCode = jsonObject["QuestionCode"].ToString(),
                        Answer = jsonObject["Answer"].Value<decimal>()
                    };
                case QuestionType.Choice:
                    return new ChoiceQuestionAnswer
                    {
                        QuestionCode = jsonObject["QuestionCode"].ToString(),
                        Answer = jsonObject["Answer"].ToString()
                    };
                default:
                    throw new ApplicationException($"Unexpected question type {typeName}");
            }
        }
    }
}
