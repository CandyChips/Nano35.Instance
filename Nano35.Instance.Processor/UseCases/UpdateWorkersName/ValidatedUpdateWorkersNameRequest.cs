﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersName
{
    public class UpdateWorkersNameValidatorErrorResult : 
        IUpdateWorkersNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateWorkersNameRequest:
        IPipelineNode<
            IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract>
    {
        private readonly IPipelineNode<
            IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract> _nextNode;

        public ValidatedUpdateWorkersNameRequest(
            IPipelineNode<
                IUpdateWorkersNameRequestContract,
                IUpdateWorkersNameResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateWorkersNameResultContract> Ask(
            IUpdateWorkersNameRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateWorkersNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}