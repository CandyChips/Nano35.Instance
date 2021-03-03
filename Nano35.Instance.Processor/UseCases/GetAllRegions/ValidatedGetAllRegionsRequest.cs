﻿using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllRegions
{
    public class GetAllRegionsValidatorErrorResult :
        IGetAllRegionsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllRegionsRequest:
        IPipelineNode<
            IGetAllRegionsRequestContract, 
            IGetAllRegionsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllRegionsRequestContract, 
            IGetAllRegionsResultContract> _nextNode;

        public ValidatedGetAllRegionsRequest(
            IPipelineNode<
                IGetAllRegionsRequestContract, 
                IGetAllRegionsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllRegionsResultContract> Ask(
            IGetAllRegionsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllRegionsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}