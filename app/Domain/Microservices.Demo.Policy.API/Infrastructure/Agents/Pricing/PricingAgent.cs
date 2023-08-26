using Microservices.Demo.Policy.API.CQRS.Commands.Infrastructure.Dtos.Offer;
using Microservices.Demo.Policy.API.Domain.Entities;
using Microservices.Demo.Policy.API.Infrastructure.Agents.Pricing.Commands;
using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.Infrastructure.Agents.Pricing
{
    public class PricingAgent : IPricingAgent
    {
        private readonly IPricingClient pricingClient;

        public PricingAgent(IPricingClient pricingClient)
        {
            this.pricingClient = pricingClient;
        }

        public async Task<Price> CalculatePrice(PricingParams pricingParams)
        {
            var cmd = new CalculatePriceCommand
            {
                ProductCode = pricingParams.ProductCode,
                PolicyFrom = pricingParams.PolicyFrom,
                PolicyTo = pricingParams.PolicyTo,
                SelectedCovers = pricingParams.SelectedCovers,
                Answers = ConstructAnswers(pricingParams.Answers)
            };

            var result = await pricingClient.CalculatePrice(cmd);

            return new Price(result.CoverPrices);
        }

        private List<QuestionAnswerDto> ConstructAnswers(List<Answer> answers)
        {
            return answers.Select(a => ToQuestionAnswer(a)).ToList();
        }

        private QuestionAnswerDto ToQuestionAnswer(Answer a)
        {
            if (a is TextAnswer)
            {
                return new TextQuestionAnswer
                {
                    QuestionCode = a.QuestionCode,
                    Answer = (string)a.GetAnswerValue()
                };
            }

            if (a is ChoiceAnswer)
            {
                return new ChoiceQuestionAnswer
                {
                    QuestionCode = a.QuestionCode,
                    Answer = (string)a.GetAnswerValue()
                };
            }

            if (a is NumericAnswer)
            {
                return new NumericQuestionAnswer
                {
                    QuestionCode = a.QuestionCode,
                    Answer = (decimal)a.GetAnswerValue()
                };
            }

            throw new ArgumentException("Unexpectd answer type " + a.GetType().Name);
        }
    }
}
