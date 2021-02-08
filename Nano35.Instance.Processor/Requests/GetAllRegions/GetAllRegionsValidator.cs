using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllRegions
{
    public class GetAllRegionsValidatorErrorResult : IGetAllRegionsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllRegionsValidator:
        IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>
    {
        private readonly IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract> _nextNode;

        public GetAllRegionsValidator(
            IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllRegionsResultContract> Ask(IGetAllRegionsRequestContract input,
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