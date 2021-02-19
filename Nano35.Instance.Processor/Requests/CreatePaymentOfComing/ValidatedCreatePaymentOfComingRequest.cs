﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreatePaymentOfComing
{
    public class CreatePaymentOfComingValidatorErrorResult : ICreatePaymentOfComingErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreatePaymentOfComingRequest:
        IPipelineNode<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract>
    {
        private readonly IPipelineNode<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract> _nextNode;

        public ValidatedCreatePaymentOfComingRequest(
            IPipelineNode<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreatePaymentOfComingResultContract> Ask(ICreatePaymentOfComingRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreatePaymentOfComingValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}