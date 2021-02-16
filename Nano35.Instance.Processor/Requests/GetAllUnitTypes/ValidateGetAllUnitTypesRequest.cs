using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllUnitTypes
{
    public class GetAllUnitTypesValidatorErrorResult : IGetAllUnitTypesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidateGetAllUnitTypesRequest:
        IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>
    {
        private readonly IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract> _nextNode;

        public ValidateGetAllUnitTypesRequest(
            IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllUnitTypesResultContract> Ask(IGetAllUnitTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllUnitTypesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}