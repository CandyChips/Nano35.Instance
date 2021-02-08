using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllInstances
{
    public class GetAllInstancesValidatorErrorResult : IGetAllInstancesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllInstancesValidator:
        IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> _nextNode;

        public GetAllInstancesValidator(
            IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllInstancesResultContract> Ask(IGetAllInstancesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllInstancesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}