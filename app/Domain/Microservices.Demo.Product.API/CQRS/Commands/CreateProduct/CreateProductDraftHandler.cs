using MediatR;
using Microservices.Demo.Product.API.CQRS.Commands.Infrastructure.Dtos.Product;
using Microservices.Demo.Product.API.Domain;
using Microservices.Demo.Product.API.Infrastructure.Data.Entities;
using Microservices.Demo.Product.API.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.Demo.Product.API.CQRS.Commands.CreateProduct
{
    public class CreateProductDraftHandler: IRequestHandler<CreateProductDraftCommand, CreateProductDraftResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductDomainService _productDomainService;

        public CreateProductDraftHandler(IProductRepository productRepository,ProductDomainService productDomainService)
        {
            _productRepository = productRepository;
            _productDomainService = productDomainService;
        }

        public async Task<CreateProductDraftResult> Handle(CreateProductDraftCommand request, CancellationToken cancellationToken)
        {
            var draft = _productDomainService.CreateDraft
            (
                request.ProductDraft.Code,
                request.ProductDraft.Name,
                request.ProductDraft.Image,
                request.ProductDraft.Description,
                request.ProductDraft.MaxNumberOfInsured
            );

            foreach (var cover in request.ProductDraft.Covers)
            {
                draft = _productDomainService.AddCover(draft,cover.Code, cover.Name, cover.Description, cover.Optional, cover.SumInsured);
            }

            var questions = new List<Question>();
            foreach (var question in request.ProductDraft.Questions)
            {
                switch (question)
                {
                    case NumericQuestionDto numericQuestion:
                        questions.Add(new NumericQuestion(numericQuestion.QuestionCode, numericQuestion.Index,
                            numericQuestion.Text));
                        break;
                    case DateQuestionDto dateQuestion:
                        questions.Add(new DateQuestion(dateQuestion.QuestionCode, dateQuestion.Index,
                            dateQuestion.Text));
                        break;
                    case ChoiceQuestionDto choiceQuestion:
                        questions.Add(new ChoiceQuestion(choiceQuestion.QuestionCode, choiceQuestion.Index,
                            choiceQuestion.Text, choiceQuestion.Choices.Select(c => new Choice(c.Code, c.Label)).ToList()));
                        break;
                }
            }
            draft = _productDomainService.AddQuestions(draft,questions);

            await _productRepository.Add(draft);

            return new CreateProductDraftResult
            {
                ProductId = draft.ProductId
            };
        }
    }
}
