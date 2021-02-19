using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRegion
{
    public class UpdateInstanceRegionValidatorErrorResult : IUpdateInstanceRegionErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceRegionRequest:
        IPipelineNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract>
    {
        private readonly IPipelineNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract> _nextNode;

        public ValidatedUpdateInstanceRegionRequest(
            IPipelineNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateInstanceRegionResultContract> Ask(
            IUpdateInstanceRegionRequestContract input)
        {
            if (false)
            {
                return new UpdateInstanceRegionValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}