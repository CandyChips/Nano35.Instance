using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests.GetAllInstances;

namespace Nano35.Instance.Api.Requests.GetAllCurrentInstances
{
    public class GetAllCurrentInstancesValidatorErrorResult : IGetAllInstancesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllCurrentInstancesRequest:
        IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> _nextNode;

        public ValidatedGetAllCurrentInstancesRequest(
            IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllInstancesResultContract> Ask(
            IGetAllInstancesRequestContract input)
        {
            if (false)
            {
                return new GetAllInstancesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}